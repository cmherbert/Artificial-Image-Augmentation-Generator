using ArtificalAugmentationGenerator.Components.LWSVR;
using ArtificalAugmentationGenerator.Components.Options;
using ArtificalAugmentationGenerator.Components.Presets.Models;
using ArtificalAugmentationGenerator.Plugins;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ArtificalAugmentationGenerator.Components.CMDLine
{
    /// <summary>
    /// Handles augmentation in non interactive mode. This class is designed for bulk processing 
    /// </summary>
    internal class ConsoleTask
    {
        internal static void PrepareConsoleRuntime(IAGOptions options)
        {
            //Console.CursorVisible = true;
            //Variables
            List<string> images = new List<string>();
            List<AugmentationPackage> augmentations = new List<AugmentationPackage>();
            List<Preset> presets = new List<Preset>();
            List<AugmentationPresetPair> effectWithPresets = new List<AugmentationPresetPair>();

            //Identify source images
            #region Print Configuration
            Console.WriteLine(string.Join(", ", options.GetConfiguration(options).Select(x => $"{x.Key}={GetValueString(x.Value ?? "Null")}")));
            Console.WriteLine();
            #endregion
            #region Source images
            Console.Write("Finding source images...   ");
            images = Directory.GetFiles(options.SourceFolder, "*", options.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
            if (options.SourceFilter != null)
            {
                images = images.Where(x => Path.GetExtension(x).Equals(options.SourceFilter.Replace("*.", ""), StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            if (options.PathFilter != null)
                images = images.Where(x =>
                     Regex.IsMatch(Path.Combine(Path.GetDirectoryName(x).Replace(options.SourceFolder, "")), options.PathFilter)).ToList();
            if (options.SourceFilterFiles.Length > 0)
            {
                List<string> masterFilter = options.SourceFilterFiles.SelectMany(x => File.ReadAllLines(x).Select(y => y.ToLower())).Distinct(StringComparer.InvariantCultureIgnoreCase).ToList();
                images = images.Where(x => masterFilter.Contains(Path.GetFileName(x.ToLower())) || masterFilter.Contains(Path.GetFullPath(x.ToLower()))).ToList();
            }
            Console.WriteLine($"Found {images.Count}!");
            #endregion
            #region load augmentations
            Console.Write("Checking for Augmentations...   ");
            if (options.Augmentations.Length == 0)
                throw new Exception("No augmentations requested");
            foreach (var effect in options.Augmentations.Select(x => x.ToUpper()).Distinct())
            {
                var fx = ContentManager.Augmentations.FirstOrDefault(x => x.Item.Name.ToUpper().Equals(effect));
                if (fx is null)
                    throw new Exception($"Augmentation {fx} was not found. Please run this program with argument '--list augmentations' to see available augmentations");
                else
                    augmentations.Add(fx);
            }
            Console.WriteLine($"Done!");
            #endregion
            #region load presets
            List<PresetXML> presetXMLS = new List<PresetXML>();
            Console.Write("Loading Presets...   ");
            foreach (var preset in options.Presets)
            {
                if (!File.Exists(preset))
                    throw new Exception($"Could not find PresetXML file \"{preset}\"");
                presetXMLS.Add(PresetXML.CreateFromFile(preset));
            }
            //Find relevant presets
            presets = presetXMLS.SelectMany(x => x.Presets).Where(x => augmentations.Select(y => y.Item.Name.ToUpper()).Contains(x.Augmentation.ToUpper())).ToList();
            //Create Standin - Default preset for augmentations without presets
            foreach (var effect in augmentations.Where(x => presets.Count(y => y.Augmentation.ToUpper().Equals(x.Item.Name.ToUpper())) == 0).ToList())
            {
                presets.Add(Preset.CreateDefault(effect.Item.Name));
            }
            Console.WriteLine($"{presets.Count} Presets Loaded using {presets.Count(x => x.IsDefault)} defaults!");
            #endregion
            #region Create augmentations using presets
            Console.Write("Preparing augmentations...   ");
            foreach (var preset in presets)
            {
                effectWithPresets.Add(new AugmentationPresetPair(augmentations.Find(x => x.Item.Name.ToUpper().Equals(preset.Augmentation.ToUpper())), preset));
            }
            Console.WriteLine("Done!");
            #endregion
            #region Create Worker Splits
            List<List<string>> threaddata;
            if (options.Workers >= 2)
            {
                threaddata = images.Section(images.Count / options.Workers).Select(x => x.ToList()).ToList();
                if (threaddata.Count > options.Workers)
                {
                    while (threaddata.Count > 2 && threaddata.Count > options.Workers)
                    {
                        threaddata[threaddata.Count - 2].AddRange(threaddata[threaddata.Count - 1]);
                        threaddata.RemoveAt(threaddata.Count - 1);
                    }
                }
            }
            else
            {
                threaddata = new List<List<string>>() { images };
            }

            #endregion
            #region Create Output Folders
            for (int i = 0; i < effectWithPresets.Count; i++)
            {
                string output = Path.Combine(options.OutputFolder, effectWithPresets[i].Preset.Name);
                Directory.CreateDirectory(output);
            }
            #endregion

            var dt_start = DateTime.Now;
            Console.WriteLine("Started at: " + dt_start);

            if (!options.WorkersUseProcess)
                RunMultiThread(options, threaddata, effectWithPresets);
            else
            {
                if (options.WP_ID == -1)
                    RunMultiProcess(options, threaddata, effectWithPresets);
                else
                {
                    RunProcess(options, threaddata[options.WP_ID], effectWithPresets);
                }
            }

            var dt_end = DateTime.Now;
            Console.WriteLine("Finished at: " + dt_end);
            Console.WriteLine("Runtime: " + (dt_end - dt_start).ToString());
            Console.WriteLine();
            Console.WriteLine("========================");
            Console.WriteLine("Done!");
            Console.WriteLine();


        }

        private static string GetValueString(object v)
        {
            if (v is string[] strarr)
            {
                return $"[{string.Join(",", strarr)}]";
            }
            else
                return v.ToString();
        }

        /// <summary>
        /// Handles in-process augmentation. 
        /// </summary>
        /// <param name="options">Program arguments</param>
        /// <param name="threaddata">Images to augment, split for desired number of threads</param>
        /// <param name="effectWithPresets">Augmentation / preset pairs</param>
        internal static void RunMultiThread(IAGOptions options, List<List<string>> threaddata, List<AugmentationPresetPair> effectWithPresets)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Begining Augmentation...");
            Console.WriteLine("========================");

            using (var ci = new ConsoleInterface_Thread())
            {

                for (int i = 0; i < effectWithPresets.Count; i++)
                {
                    if (options.OverrideOutputRestrictions && Console.IsOutputRedirected)
                    {
                        Console.WriteLine($"Processing augmentation: {effectWithPresets[i].Effect.Name}:{effectWithPresets[i].Preset.Name}");
                    }

                    //string output = Path.Combine(options.OutputFolder, effectWithPresets[i].Preset.Name);
                    //Directory.CreateDirectory(output);
                    ci.Report($"Preset {i + 1}/{effectWithPresets.Count} ({effectWithPresets[i].Effect.Name}:{effectWithPresets[i].Preset.Name})", "Progress", (double)i / effectWithPresets.Count, 0);
                    ci.SetP2Max(threaddata.Sum(x => x.Count));


                    List<Thread> tasks = new List<Thread>();
                    AutoResetEvent are = new AutoResetEvent(false);
                    object lockable = new object();
                    int completed = 0;
                    for (int j = 0; j < threaddata.Count; j++)
                    {
                        Thread t = new Thread(new ParameterizedThreadStart((x) =>
                        {
                            try
                            {
                               
                                RunThread(options, threaddata[((Tuple<int, int>)x).Item2], new List<AugmentationPresetPair>() { effectWithPresets[((Tuple<int, int>)x).Item1] }, ci);
                                //ParallelWorker(new EffectPresetPair(effectWithPresets[i]), ci, output, threaddata[j], options);
                            }
                            finally
                            {
                                lock (lockable) completed++; are.Set();
                            }
                        }));
                        tasks.Add(t);
                        t.Name = $"Artifical Augmentation Genrator Worker {j}";
                        t.Start(new Tuple<int, int>(i, j));
                    }
                    while (completed < tasks.Count)
                        are.WaitOne();

                    //images.Section(images.Count / options.Workers).AsParallel().WithDegreeOfParallelism(options.Workers).ForAll(x =>
                    //{
                    //    ParallelWorker(new EffectPresetPair(effectWithPresets[i]), ci, output, x, options);
                    //});


                    ci.Report($"Preset {i + 1}/{effectWithPresets.Count} ({effectWithPresets[i].Effect.Name}:{effectWithPresets[i].Preset.Name})", "Progress", (i + 1.0d) / effectWithPresets.Count, 1);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
        /// <summary>
        /// Handles out-of-process augmentation (CONTROLLER). 
        /// </summary>
        /// <param name="options">Program arguments</param>
        /// <param name="threaddata">Images to augment, split for desired number of threads</param>
        /// <param name="effectWithPresets">Augmentation / preset pairs</param>
        internal static void RunMultiProcess(IAGOptions options, List<List<string>> threaddata, List<AugmentationPresetPair> effectWithPresets)
        {
            //Start Server
            Console.WriteLine("Starting Server...");
            LWSVR.LWSVR svr = new LWSVR.LWSVR();
            svr.StartServer();
            Console.WriteLine("Server opened on port " + svr.Port);

            List<Process> WorkerProcIDs = new List<Process>();
            #region Create Worker Processes
            Console.WriteLine("Creating Workers...");

            int procID = Process.GetCurrentProcess().Id;
            Console.WriteLine("Platform: " + System.Environment.OSVersion.Platform.ToString());
            Console.WriteLine("Directory: \"" + Environment.CurrentDirectory + "\"");
            for (int i = 0; i < options.Workers; i++)
            {
                string args = $"--wp_id {i} --wp_parentid {procID} --wp_port {svr.Port} --workers {options.Workers} --workersuseprocess --source-folder {$"\'{options.SourceFolder}\'"} --augmentations {string.Join(" ", options.Augmentations.Select(x => $"\'{x}\'"))} --presets {string.Join(" ", options.Presets.Select(x => $"\'{x}\'"))} --output-folder {$"\'{options.OutputFolder}\'"}";
                if (options.SourceFilterFiles.Length > 0)
                    args += $" --source-filterfiles {string.Join(" ", options.SourceFilterFiles.Select(x => $"\'{x}\'"))}";
                if (options.SourceLabel != null)
                    args += $" --source-labels {$"\'{options.SourceLabel}\'"}";
                if (options.SourceFilter != null)
                    args += $" --source-filter {$"\'{options.SourceFilter}\'"}";
                if (options.PathFilter != null)
                    args += $" --path-filter {$"\'{options.PathFilter}\'"}";
                if (options.PreserveFolderStructure)
                    args += $" --preserve-fs";
                if (options.Recursive)
                    args += $" --recursive";
                if (System.Environment.OSVersion.Platform != PlatformID.Win32NT)
                {
                    Process p = new Process()
                    {
                        //This approach was used to ensure program started in conda environment
                        StartInfo = new ProcessStartInfo("/bin/bash", $"-c \"mono ArtificalAugmentationGenerator.exe {args}\""/*> worker_{i}.txt"*/)
                        {
                            WorkingDirectory = Environment.CurrentDirectory,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                        }
                        ,
                        EnableRaisingEvents = true
                    };


                    //p.Exited += (s, e) =>
                    //{
                    //    Console.WriteLine($"conda activate CLR && mono ImageArtefactGenerator.exe {args}");
                    //    Console.WriteLine($"Info: \"{p.StandardOutput.ReadToEnd()}\"");
                    //    Console.WriteLine($"Error: \"{p.StandardError.ReadToEnd()}\"");
                    //};
                    p.Start();

                    WorkerProcIDs.Add(p);
                }
                else
                {
                    Process p = new Process()
                    {
                        StartInfo = new ProcessStartInfo("ArtificalAugmentationGenerator.exe", $"{args.Replace('\'', '"')}")
                        { 
                            WindowStyle = ProcessWindowStyle.Hidden,
                            //CreateNoWindow = true,
                            //UseShellExecute = false,
                            WorkingDirectory = Environment.CurrentDirectory,

                        },
                        EnableRaisingEvents = true
                    };
                    p.Start();
                    WorkerProcIDs.Add(p);
                }
            }
            #endregion

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Begining Augmentation...");
            Console.WriteLine("========================");

            int completed = 0;

            //Start Monitoring Processes
            using (ConsoleInterface_Process ciproc = new ConsoleInterface_Process())
            {
                for (int i = 0; i < threaddata.Count; i++)
                {
                    ciproc.CreateWorker($"Worker {i}\t ({WorkerProcIDs[i].Id})", effectWithPresets.Count * threaddata[i].Count);
                    //WorkerProcIDs[i].Exited += (s, e) => ciproc.Report(i, "Finished: " + WorkerProcIDs[i].ExitCode);
                }
                svr.ClientUpdate += (s, id, msg, val) => { ciproc.Report(id, msg == 0 ? val : effectWithPresets.Count * threaddata[id].Count); if (msg == 1) completed++; };
                while (completed < WorkerProcIDs.Count)
                {
                    Thread.Sleep(2000);
                }

            }
        }

        internal static void RunThread(IAGOptions options, List<string> images, List<AugmentationPresetPair> effectPresetPairs, ConsoleInterface_Thread ci)
        {
            foreach (var epp in effectPresetPairs)
            {
                string output = Path.Combine(options.OutputFolder, epp.Preset.Name);
                ParallelWorker(epp, (x) => ci.Report(), output, images, options);
            }
        }
        internal static void RunProcess(IAGOptions options, List<string> images, List<AugmentationPresetPair> effectPresetPairs)
        {
            //Rename Window
            Console.Title = "Artifical Augmentation Generator Worker " + options.WP_ID;
            //Monitor Parent Closure
            Process p = Process.GetProcessById(options.WP_Parent);
            p.EnableRaisingEvents = true;
            p.Exited += (s, e) => { Console.WriteLine("Parent Terminated, Terminating..."); Environment.Exit(1); };

            LWSVR.LWSVR server = new LWSVR.LWSVR(options.WP_Port);
            int count = 0;
            foreach (var epp in effectPresetPairs)
            {
                string output = Path.Combine(options.OutputFolder, epp.Preset.Name);
                ParallelWorker(epp, (x) => { count++; if (count % 10 == 0) server.SendUpdate(options.WP_ID, count); }, output, images, options);
            }
            server.SendClosure(options.WP_ID);
        }

        private static void ParallelWorker(AugmentationPresetPair epp, Action<int> Report, string output, IEnumerable<string> data, IAGOptions options)
        {
            try
            {
                //Create Effect Processor
                IAugmentationProcessor processor = (IAugmentationProcessor)Activator.CreateInstance(epp.Effect.Processor, epp.Effect);

                int counter = 0;

                foreach (var image in data)
                {

                    string fout = "";
                    if (options.PreserveFolderStructure && options.Recursive)
                    {
                        string rpath = Path.GetDirectoryName(Path.GetFullPath(image).ToLower().Replace(options.SourceFolder.ToLower(), ""));
                        if (rpath.StartsWith("\\") || rpath.StartsWith("/"))
                            rpath = rpath.Substring(1);
                        Directory.CreateDirectory(Path.Combine(output, rpath));
                        fout = Path.Combine(output, rpath, Path.GetFileName(image));
                    }
                    else
                        fout = Path.Combine(output, Path.GetFileName(image));
                    if (options.MissingOnly && File.Exists(fout))
                    {
                        Report?.Invoke(1);
                        continue;
                    }
                    else
                    {
                        epp.ApplySmallRandomisation(processor.Properties);
                        using (Mat mat = new Mat(image))
                        {
                            mat.AddAlphaChannel();
                            var epr = processor.ProcessImage(mat);
                            CreateImageV2(epr, mat).SaveImage(fout);
                            epr.Dispose();

                        }
                        //OpenCVSharp keeps some Mats alive causing memory leak (Esp. when adding scalars)
                        if (counter++ % 50 == 0)
                            GC.Collect();
                        Report?.Invoke(1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private static Mat CreateImageV2(AugmentationProcessorResult epr, Mat input)
        {
            if (!epr.SupportsLayering)
            {
                return epr.Layers[0].Layer;
            }
            else
            {
                //Ensure alpha channel
                input.AddAlphaChannel();
                foreach (var layer in epr.Layers)
                {
                    input.MergeSubMat(layer.Layer, 0, 0);
                }
                return input;
            }
        }

        private static Bitmap CreateImage(AugmentationProcessorResult epr, OpenCvSharp.Mat input)
        {
            if (!epr.SupportsLayering)
                return epr.Layers[0].Layer.ToBitmap();
            //if (input.Channels() == 3) {
            //    var m1 = input.Split();
            //    OpenCvSharp.Cv2.Merge(new OpenCvSharp.Mat[] { m1[0], m1[1], m1[2], OpenCvSharp.Mat.Ones(input.Rows, input.Cols, OpenCvSharp.MatType.CV_8UC1) },input);
            //    //input.ConvertTo(input,OpenCvSharp.MatType.CV_8UC4);
            //}
            //foreach (var layer in epr.Layers)
            //    OpenCvSharp.Cv2.Add(input, layer.Layer, input);
            else
            {
                var mbmp = input.ToBitmap();
                using (var g = Graphics.FromImage(mbmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    foreach (var layer in epr.Layers)
                    {
                        var xbmp = layer.Layer.ToBitmap();
                        {
                            g.DrawImage(xbmp, new Rectangle(0, 0, input.Width, input.Height));
                        }
                    }

                }
                return mbmp;
            }

        }
    }
}
