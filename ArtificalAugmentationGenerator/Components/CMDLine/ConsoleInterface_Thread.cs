using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.CMDLine
{
    //Adapted from:                 https://gist.github.com/DanielSWolf/0ab6a96899cc5377bf54
    //original license:             https://opensource.org/license/mit/
    /// <summary>
    /// Displays progress of augmentation when in single process mode
    /// </summary>
    internal class ConsoleInterface_Thread : IDisposable
    {
        private const int blockCount = 20;
        private readonly TimeSpan animationInterval = TimeSpan.FromSeconds(1.0 / 8);
        private const string animation = @"|/-\";
        private const ConsoleColor Accent = ConsoleColor.Gray;
        private const ConsoleColor Background = ConsoleColor.DarkGray;

        private char consoleChar1 = '■';
        private char consoleChar2 = '■';

        private readonly Timer timer;

        private double p1Progress = 0;
        private double p2Progress = 0;
        private int p2Max = 100;
        private int p2Count = 0;
        private string p1Text = "";
        private string p2Text = "";
        private int cLen = 0;
        private ConsoleColor original;
        private int lastCTop = -1;
        private int lastWTop = -1;

        private object assignment = new object();

        private double currentProgress = 0;
        private string currentText = string.Empty;
        private bool disposed = false;
        private int animationIndex = 0;

        public ConsoleInterface_Thread(bool overrideOutputRestrictions = false)
        {

            original = Console.ForegroundColor;
            timer = new Timer(TimerHandler);

            // A progress bar is only for temporary display in a console window.
            // If the console output is redirected to a file, draw nothing.
            // Otherwise, we'll end up with a lot of garbage in the target file.
            if (!Console.IsOutputRedirected)
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

        public void Report(string txt1, string txt2, double p1, double p2)
        {
            // Make sure value is in [0..1] range
            p1 = Math.Max(0, Math.Min(1, p1));
            p2 = Math.Max(0, Math.Min(1, p2));
            Interlocked.Exchange(ref p1Progress, p1);
            Interlocked.Exchange(ref p2Progress, p2);
            Interlocked.Exchange(ref p1Text, txt1);
            Interlocked.Exchange(ref p2Text, txt2);
        }
        public void Report()
        {
            // Make sure value is in [0..1] range
            lock (assignment)
            {
                p2Count++;
                var p2 = Math.Max(0, Math.Min(1, ((double)p2Count / p2Max)));
                p2Progress = p2;
            }
        }

        public void SetP2Max(int max)
        {
            p2Max = max;
            p2Count = 0;
        }

        private void TimerHandler(object state)
        {
            lock (timer)
            {
                if (disposed) return;

                int progressBlockCount = (int)(currentProgress * blockCount);
                int percent = (int)(currentProgress * 100);

                string tx1 = $"{p1Text}";
                string tx2 = $"{new string(consoleChar1, (int)(p1Progress * blockCount))}~{new string(consoleChar2, (int)(blockCount - (int)(p1Progress * blockCount)))}~ {(int)(p1Progress * 100)}%";
                string tx3 = $"{p2Text}";
                string tx4 = $"{new string(consoleChar1, (int)(p2Progress * blockCount))}~{new string(consoleChar2, (int)(blockCount - (int)(p2Progress * blockCount)))}~ {(int)(p2Progress * 100)}%";
                UpdateText(new string[] { tx1, tx2, tx3, tx4 });

                ResetTimer();
            }

        }

        private void UpdateText(string[] text)
        {
            int offset = 0;
            if (Console.CursorTop == 0 && Console.WindowTop == 0)
            {
                //Something weird has happened, and buffer has cleared. Have not figured this out yet

            }
            else if (lastCTop == Console.CursorTop)
            {
                //Assume nothing has been written

                //Offset is needed to ensure we are not writing outside of the screen. It gets crashy.
                offset = cLen - Math.Max(Math.Min(cLen, Console.CursorTop - cLen), 0);
                if (offset < cLen)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - (cLen - offset));
                    for (int i = 0; i < cLen - offset; i++)
                    {
                        Console.Write(new string(' ', Console.WindowWidth));
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - (cLen - offset));
                }
            }
            else
            {
                //Continue from where left off
            }
            for (int i = offset; i < text.Length; i++)
            {
                if (text[i].Contains("~"))
                {
                    var x = text[i].Split('~');
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
                    Console.WriteLine(text[i]);
                }

            }
            
            lastCTop = Console.CursorTop;
            lastWTop = Console.WindowTop;
            cLen = text.Length;




        }


        private void ResetTimer()
        {
            timer.Change(animationInterval, TimeSpan.FromMilliseconds(-1));
        }

        public void Dispose()
        {
            lock (timer)
            {
                disposed = true;
                Console.SetCursorPosition(0, Console.CursorTop + cLen);
                Console.ForegroundColor = original;
                Console.CursorVisible = true;
            }
        }
    }
}
