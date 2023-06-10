using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAG_Water.Types
{
    internal class RainDrop
    {
        public Rectangle Bounds => Drops.Count == 1 ? Drops.First() : Drops.Count > 0 ? CalculateBounds() : Rectangle.Empty;


        public List<Rectangle> Drops = new List<Rectangle>();

        public RainDrop(int sx, int sy, int sw, int sh)
        {
            Drops.Add(new Rectangle(sx, sy, sw, sh));
        }

        private Rectangle CalculateBounds()
        {
            int sx = Drops[0].X, sy = Drops[0].Y, sw = Drops[0].Width - Drops[0].X, sh = Drops[0].Height - Drops[0].Y;
            foreach (var drop in Drops)
            {
                sx = Math.Min(drop.X, sx);
                sy = Math.Min(drop.Y, sy);
                sw = Math.Max(drop.X + drop.Width, sw);
                sh = Math.Max(drop.Y + drop.Height, sh);
            }
            return new Rectangle(sx, sy, sw - sx, sh - sy);
        }

        public void AddRange(List<RainDrop> drops)
        {
            Drops.AddRange(drops.Select(x => x.Bounds).ToArray());
        }

        internal void AddRandom(int sw, int sh, Random rand)
        {
            int width = Drops[0].Width;
            int height = Drops[0].Height;
            int count = 0;
            //Pick random location
            var angle = (double)rand.Next(0, 360);
            angle = angle * (Math.PI / 180);

            var nsx = Math.Cos(angle);
            var nsy = Math.Sin(angle);
            while (true)
            {

                var nRect = new Rectangle((int)(nsx * count) + Drops[0].X, (int)(nsy * count) + Drops[0].Y, sw, sh);
                if (Drops.Count(x => x.IntersectsWith(nRect)) > 0)
                {
                    count++;
                    continue;
                }
                else
                    Drops.Add(nRect);
                return;
            }
        }
    }
}
