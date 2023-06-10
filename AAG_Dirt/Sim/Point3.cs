using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AAG_Dirt.Sim
{
    internal class Point3
    {
        double x, y, z;
        static readonly string[] axes = new string[] { "X", "Y", "Z" };
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }

        public Point3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /// <summary>
        /// Name of all axis supported by this type
        /// An overcomplicated solution to an otherwise simple problem
        /// </summary>
        public string[] Axes => axes;

        public static Point3 operator +(Point3 lhs, Point3 rhs)
        {
            return new Point3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }
        public static Point3 operator -(Point3 lhs, Point3 rhs)
        {
            return new Point3(lhs.x + rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }
        public static Point3 operator /(Point3 lhs, Point3 rhs)
        {
            return new Point3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
        }
        public static Point3 operator *(Point3 lhs, Point3 rhs)
        {
            return new Point3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
        }

        public static Point3 operator +(Point3 trgt, double constant)
        {
            return new Point3(trgt.X + constant, trgt.Y + constant, trgt.Z + constant);
        }
        public static Point3 operator -(Point3 trgt, double constant)
        {
            return new Point3(trgt.X - constant, trgt.Y - constant, trgt.Z - constant);
        }
        public static Point3 operator *(Point3 trgt, double constant)
        {
            return new Point3(trgt.X * constant, trgt.Y * constant, trgt.Z * constant);
        }
        public static Point3 operator /(Point3 trgt, double constant)
        {
            return new Point3(trgt.X / constant, trgt.Y / constant, trgt.Z / constant);
        }

        public override string ToString()
        {
            return $"x: {x}, y: {y}, z: {z}";
        }

        public double this[string axis]
        {
            get
            {
                if (axes.Contains(axis))
                    return (double)typeof(Point3).GetProperty(axis).GetValue(this);
                else
                    throw new NotImplementedException();
            }
            set
            {
                if (axes.Contains(axis))
                    typeof(Point3).GetProperty(axis).SetValue(this, value);
                else
                    throw new NotImplementedException();
            }
        }
    }
}
