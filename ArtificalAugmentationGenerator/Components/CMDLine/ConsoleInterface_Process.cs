using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ArtificalAugmentationGenerator.Components.CMDLine
{
    //Adapted from:             https://gist.github.com/DanielSWolf/0ab6a96899cc5377bf54
    //original license:         https://opensource.org/license/mit/
    /// <summary>
    /// Displays progress of seperate-process workers in console 
    /// </summary>
    internal class ConsoleInterface_Process : IDisposable
    {
        private const int blockCount = 20;
        private readonly TimeSpan animationInterval = TimeSpan.FromSeconds(2.0);
        private const string animation = @"|/-\";
        private const ConsoleColor Accent = ConsoleColor.Gray;
        private const ConsoleColor Background = ConsoleColor.DarkGray;

        private readonly Timer timer;

        //private double p1Progress = 0;
        //private double p2Progress = 0;
        //private int p2Max = 100;
        //private int p2Count = 0;
        //private string p1Text = "";
        //private string p2Text = "";
        private int cLen = 0;
        List<ProcMonLine> lines = new List<ProcMonLine>();
        private ConsoleColor original;

        private bool disposed = false;


        public ConsoleInterface_Process(bool overrideOutputRestrictions = false)
        {

            original = Console.ForegroundColor;
            timer = new Timer(TimerHandler);

            // A progress bar is only for temporary display in a console window.
            // If the console output is redirected to a file, draw nothing.
            // Otherwise, we'll end up with a lot of garbage in the target file.
            //if (!Console.IsOutputRedirected)
            {
                //Console.CursorVisible = false;
                ResetTimer();
            }
            //if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    consoleChar1 = '#';
            //    consoleChar2 = ' ';
            //}

        }

        public void CreateWorker(string workername, int maxvalue)
        {
            lock (lines)
            {
                lines.Add(new ProcMonLine() { CurrentText = workername, CurrentMax = maxvalue, CurrentValue = 0, MiscText = "Running..." });
            }
        }
        public void Report(int worker, int newvalue)
        {
            // Make sure value is in [0..1] range
            lock (lines)
            {
                lines[worker].CurrentValue = newvalue;
            }
        }
        public void Report(int worker, string newMisc)
        {
            // Make sure value is in [0..1] range
            lock (lines)
            {
                lines[worker].MiscText = newMisc;
            }
        }

        private void TimerHandler(object state)
        {
            lock (timer)
            {
                if (disposed) return;
                foreach (var line in lines)
                    line.UpdateText();
                
                UpdateText();

                ResetTimer();
            }

        }

        private void UpdateText()
        {
            if (!Console.IsOutputRedirected)
            {
                foreach (var line in lines)
                {
                    if (line.DisplayString.Contains("~"))
                    {
                        var x = line.DisplayString.Split('~');
                        Console.ForegroundColor = Accent;
                        Console.Write(x[0]);
                        Console.ForegroundColor = Background;
                        Console.Write(x[1]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(x[2]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(line);
                    }
                }


                if (lines.Count < cLen)
                    for (int i = 0; i < (cLen - lines.Count); i++)
                        Console.WriteLine();
                Console.SetCursorPosition(0, Console.CursorTop - Math.Max(lines.Count, cLen));

                cLen = lines.Count;

            }
            else
            {
                //Clear cLen
                Console.WriteLine(new string('\b', cLen));
                cLen = 0;
                string dtnow = $"==============================={Environment.NewLine}Status as of {DateTime.Now.ToString()}";
                Console.WriteLine(dtnow);
                cLen += dtnow.Length + Environment.NewLine.Length;
                foreach (var line in lines)
                {
                    string dr = line.DisplayString.Replace("~", "");
                    Console.WriteLine(dr);
                    cLen += dr.Length + Environment.NewLine.Length;
                }
            }
        }

        private void ResetTimer()
        {
            timer.Change(Console.IsOutputRedirected ? TimeSpan.FromMinutes(5) : animationInterval, TimeSpan.FromMilliseconds(-1));
        }

        public void Dispose()
        {
            lock (timer)
            {
                disposed = true;
                if (!Console.IsOutputRedirected)
                {
                    Console.SetCursorPosition(0, Console.CursorTop + cLen);
                    Console.ForegroundColor = original;
                }

            }
        }

        private class ProcMonLine
        {
            const string consoleChar1 = "■#";
            const string consoleChar2 = "■ ";
            private string _displayString = "";

            public string CurrentText { get; set; }
            public string MiscText { get; set; }
            public int CurrentValue { get; set; }
            public int CurrentMax { get; set; }

            public string DisplayString => _displayString;

            internal void UpdateText()
            {
                double percent = Math.Min(1, Math.Max(0, (double)CurrentValue / CurrentMax));
                var nstr = $"{CurrentText}  {new string(consoleChar1[Console.IsOutputRedirected ? 1 : 0], (int)(percent * blockCount))}~{new string(consoleChar2[Console.IsOutputRedirected ? 1 : 0], (int)(blockCount - (int)(percent * blockCount)))}~ {(int)(percent * 100)}% {MiscText}";
                if (_displayString.Length > nstr.Length)
                    nstr += new string(' ', _displayString.Length - nstr.Length);
                _displayString = nstr;
            }
        }
    }


}
