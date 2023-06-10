using ArtificalAugmentationGenerator.Components;
using ArtificalAugmentationGenerator.Components.Interface.Designer.Types;
using ArtificalAugmentationGenerator.Components.Options;
using ArtificalAugmentationGenerator.Plugins;
using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator
{
    
    internal class Program
    {
        static IAGOptions _options;

        [STAThread]
        static void Main(string[] args)
        {
            //Debugger.Launch();
            _options = IAGOptions.CreateOptionsClass<IAGOptions>(args);
            if (_options == null)
            {
                return;
            }
            //Load Effects && Ensure success
            if (ContentManager.LoadFailed)
            {
                

                Console.WriteLine("Generator was unable to load some effects. Operation was cancelled");
                Console.WriteLine();

                foreach (var problem in ContentManager.Problems)
                {
                    Console.WriteLine(problem);
                    Console.WriteLine();
                }
                Console.WriteLine();

                //Ensure user can read message
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            }
            else if (_options.ListEffects != null)
            {
                if (_options.ListEffects.Length == 0)
                {
                    Console.WriteLine("List Options:");
                    Console.WriteLine();
                    Console.WriteLine($"{PaddString("List all:", 16)} 'all', '*'");
                    Console.WriteLine($"{PaddString("Augmentations:", 16)} 'augmentation', 'augmentations'");
                    Console.WriteLine($"{PaddString("Preset Types:", 16)} 'presettype', 'presettypes'");
                    Console.WriteLine($"{PaddString("Processors:", 16)} 'processor', 'processors'");
                }
                else
                {
                    foreach (var type in _options.ListEffects)
                    {
                        switch (type.Trim().ToUpper())
                        {
                            case "ALL":
                            case "*":
                                PrintContentTable<IAugmentation>("Effects", ContentManager.Augmentations, x => x.Name.ToUpper());
                                PrintContentTable<IPresetType>("Preset Types", ContentManager.PresetTypes, x => $"{x.Name} ({x.TypeID})");
                                PrintContentTable<Type>("Processors", ContentManager.ProcessorPackages, x => $" {x.Name} ({x.BaseType?.GenericTypeArguments[0].Name ?? "??"})");
                                break;
                            case "AUGMENTATIONS":
                            case "AUGMENTATION":
                                PrintContentTable<IAugmentation>("Effects", ContentManager.Augmentations, x => x.Name);
                                break;
                            case "PRESETTYPES":
                            case "PRESETTYPE":
                                PrintContentTable<IPresetType>("Preset Types", ContentManager.PresetTypes, x => $" {x.Name} [{x.TypeID}]");
                                break;
                            case "PROCESSORS":
                            case "PROCESSOR":
                                PrintContentTable<Type>("Processors", ContentManager.ProcessorPackages, x => new FriendlyTypeWrapper(x).Name);
                                break;
                        }
                    }
                }
            }
            else if (_options.Interactive)
            {
                //Launch User Interface
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(true);
                Application.Run(new Components.Interface.MainForm(_options));
            }
            else
            {
                
                Components.CMDLine.ConsoleTask.PrepareConsoleRuntime(_options);
            }

           
        }

        private static string PaddString(string v1, int v2)
        {
            if(v1.Length < v2)
                return v1.PadRight(v2);
            if (v1.Length > v2)
                return v1.Substring(0, v2-1);
            return v1;
        }

        private static void PrintContentTable<T>(string name, IEnumerable<IContentPackage<T>> contentList, Func<T, string> getName)
        {
            Console.WriteLine();
            Console.WriteLine($"{name} list: ({contentList.Count()})");
            Console.WriteLine();
            //Calc length for formatting
            int l = 0;
            foreach (var content in contentList)
            {
                l = Math.Max(l, getName(content.Item).Length);
            }

            foreach (var content in contentList)
            {
                Console.Write($"{getName(content.Item)} {new string(' ', l - getName(content.Item).Length)}    {(content.Builtin ? "BUILTIN" : "PLUG IN")}    ");
                if (!content.Builtin)
                {
                    int x = Console.CursorLeft;
                    int w = Console.BufferWidth - x - 1;
                    int r = 0;
                    var str = content.Source.Replace(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "");
                    while (r < str.Length)
                    {
                        Console.SetCursorPosition(x, Console.CursorTop);
                        var hs = str.Substring(r, Math.Min(str.Length - r, w));
                        Console.WriteLine(hs);
                        r += hs.Length;
                    }
                }
                else
                    Console.WriteLine();

            }
            Console.WriteLine();
        }
    }
}
