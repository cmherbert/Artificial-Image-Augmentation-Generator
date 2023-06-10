using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAG_Dirt.Sim
{
    /// <summary>
    /// Static class used to perform more natural particle scattering
    /// </summary>
    internal static class World
    {

        const double WEIGHT_MIN = 1.0;
        const double WEIGHT_MAX = 5.0;
        const double SPEED_MIN = 10.0;
        const double SPEED_MAX = 40.0;
        const double COLLISION_WEIGHT_MULTIPLIER = 3.0;

        public static List<Point3> Simulate(int particleCount, Point3[] origin, Point3 worldMin, Point3 worldMax, Random random = null)
        {
            //Create Random
            Random rand = random ?? new Random();


            //Create Particles
            Particle[] particles = new Particle[particleCount];
            for (int i = 0; i < particleCount; i++)
            {
                particles[i] = new Particle()
                {
                    Position = new Point3( rand.Next((int)worldMin.X+1, (int)worldMax.X-1), rand.Next((int)worldMin.Y+1, (int)worldMax.Y-1), worldMax.Z-1 ),
                    Speed = random.Next(10, 40) * (worldMax.Z/100),
                    Weight = random.Next(1, 5)
                };
            }

            //Process
            for (int i = 0; i < origin.Length; i++)
            {
                foreach(Particle particle in particles)
                {
                    #region Calculate new position
                    if (particle.Position.Z < worldMin.Z && !particle.HasCollided)
                        particle.HasCollided = true;

                    if (particle.HasCollided)
                    {
                        foreach (string axis in particle.Direction.Axes)
                        {
                            if (particle.Position[axis] > worldMax[axis] || particle.Position[axis] <= worldMin[axis])
                            {
                                particle.Direction[axis] = rand.NextDouble() * -1 * particle.Direction[axis] * particle.Weight / 110f;
                                particle.Speed /= particle.Weight * COLLISION_WEIGHT_MULTIPLIER;
                            }
                        }
                    }
                    else
                    {
                        foreach (string axis in particle.Position.Axes)
                        {
                            //If particle position for current axis is inline with origin on same axis, stop movement on this axis
                            if (particle.Position[axis] == origin[i][axis])
                                particle.Direction[axis] = 0;
                            //If particle position for current axis is less than origin on same axis, move at a random positive increment
                            else if (particle.Position[axis] < origin[i][axis])
                                particle.Direction[axis] = rand.NextDouble();
                            //If particle position for current axis is mroe than origin on same axis, move at a random negative increment
                            else
                                particle.Direction[axis] = rand.NextDouble() * -1;
                        }
                    }
                    #endregion
                    #region Move particle
                    particle.Position += particle.Direction * particle.Speed * particle.Weight;
                    #endregion
                }
            }

            //Return
            
            return particles.Select(x => { var p = x.Position; p.Z = 0; return p; }).ToList();
        }

  
    }
}
