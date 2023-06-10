using AAG_Water.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AAG_Water
{
    internal static class CommonExts
    {
        public static OpenCvSharp.Size ConvertToCV2(this System.Drawing.Size size)
        {
            return new OpenCvSharp.Size(size.Width, size.Height);
        }
        public static OpenCvSharp.Rect ConvertToCV2(this System.Drawing.Rectangle rect)
        {
            return new OpenCvSharp.Rect(rect.X, rect.Y, rect.Width, rect.Height);
        }
        public static List<RainDrop> Merge(this List<RainDrop> rainDrops, int tolerance = 10)
        {
            //Collect Drops

            List<List<RainDrop>> drops = new List<List<RainDrop>>();
            for (int i = 0; i < rainDrops.Count; i++)
            {
                int dIdx = drops.IndexOf(drops.Find(x => x.Contains(rainDrops[i])));
                if (dIdx == -1)
                {
                    drops.Add(new List<RainDrop> { rainDrops[i] });
                    dIdx = drops.Count - 1;
                }
                else
                {

                }

                for (int j = i; j < rainDrops.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (!drops[dIdx].Contains(rainDrops[j]))
                        if (rainDrops[i].Bounds.Inflate(tolerance).IntersectsWith(rainDrops[j].Bounds.Inflate(tolerance)))
                        {
                            drops[dIdx].Add(rainDrops[j]);
                        }
                }
            }
            //Collect Raindrops
            return drops.Select(x => { x.First().AddRange(x.Skip(1).ToList()); return x.First(); }).ToList();
        }
        public static List<RainDrop> Crop(this List<RainDrop> rainDrops, int sxMax, int syMax)
        {
            foreach (var drop in rainDrops)
            {
                drop.Drops.RemoveAll(x => x.Right >= sxMax || x.Bottom >= syMax || x.Top < 0 || x.Left < 0);
            }
            //Collect Raindrops
            rainDrops.RemoveAll(x => x.Bounds.Width == 0 || x.Bounds.Height == 0);
            return rainDrops;
        }

        internal static Rectangle Inflate(this Rectangle rect, int size)
        {
            return new Rectangle(rect.X - size / 2, rect.Y - size / 2, rect.Width + size, rect.Height + size);
        }
    }
}
