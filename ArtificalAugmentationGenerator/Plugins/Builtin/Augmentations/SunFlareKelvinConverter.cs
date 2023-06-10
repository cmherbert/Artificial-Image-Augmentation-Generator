using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.Augmentations
{
    /// <summary>
    /// Based on
    /// https://www.zombieprototypes.com/?p=210
    /// http://web.archive.org/web/20220618201011/https://www.zombieprototypes.com/?p=210
    /// </summary>
    internal static class SunFlareKelvinConverter
    {
        readonly static double[] a = new double[] { 351.97690566805693, -155.25485562709179, 325.4494125711974, -254.76935184120902 };
        readonly static double[] b = new double[] { 0.114206453784165, -0.44596950469579133, 0.07943456536662342, 0.8274096064007395 };
        readonly static double[] c = new double[] { -40.25366309332127, 104.49216199393888, -28.0852963507957, 115.67994401066147 };
        public static Color FromKelvin(int k)
        {
            int nr = 0;
            int ng = 0;
            int nb = 0;

            //RED
            if (k >= 6600)
            {
                double x_r = (k / 100) - 55;
                double x_g = (k / 100) - 50;
                nr = (int)(a[0] + b[0] * x_r + c[0] * Math.Log(x_r));
                ng = (int)(a[2] + b[2] * x_g + c[2] * Math.Log(x_g));
                nb = 255;
            }
            else
            {
                double x_g = (k / 100) - 2;
                double x_b = (k / 100) - 10;
                nr = 255;
                ng = k > 1000 ? (int)(a[1] + b[1] * x_g + c[1] * Math.Log(x_g)) : 0;
                nb = k > 2000 ? (int)(a[3] + b[3] * x_b + c[3] * Math.Log(x_b)) : 0;

            }
            return Color.FromArgb(255, Clamp(nr, 0, 255), Clamp(ng, 0, 255), Clamp(nb, 0, 255));

        }

        private static int Clamp(int x, int min, int max)
        {
            return Math.Max(min, Math.Min(max, x));
        }
    }
}
