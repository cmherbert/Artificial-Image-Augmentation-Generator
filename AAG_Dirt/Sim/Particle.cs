using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAG_Dirt.Sim
{
    internal class Particle
    {
        public Point3 Direction { get; set; }
        public Point3 Position { get; set; }
        public double Weight { get; set; }
        public double Speed { get; set; } = 0.0d;
        public bool HasCollided { get; set; } = false;
        public Particle()
        {
            Direction = new Point3(0, 0, 0);
            Position = new Point3(0, 0, 0);
        }
    }
}
