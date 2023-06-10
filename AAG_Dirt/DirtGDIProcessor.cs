using AAG_Dirt.Sim;
using ArtificalAugmentationGenerator.Plugins;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp.Extensions;

namespace AAG_Dirt
{
    /// <summary>
    /// GDI+ Implementation of Dirt Processor
    /// IT IS STRONGLY RECOMMENDED YOU USE THIS VERSION, OpenCV version is slow
    /// </summary>
    internal class DirtGDIProcessor : AugmentationProcessor<DirtAugmentation>
    {
        public DirtGDIProcessor(DirtAugmentation effect) : base(effect)
        {

        }

        public override AugmentationProcessorResult ProcessImage(Mat image)
        {

            AugmentationProcessorResult epr = new AugmentationProcessorResult(true);
            for (int i = 0; i < properties.DirtPatches; i++)
                GenerateOverlay(epr, image.Width, image.Height);
            return epr;

        }

        private void GenerateOverlay(AugmentationProcessorResult epr, int width, int height)
        {
            Mat l1;
            int sw = (int)(244 * properties.Scale), sh = (int)(244 * properties.Scale);
            using (var bmp = new Bitmap(sw, sh, PixelFormat.Format32bppArgb))
            using (var g = Graphics.FromImage(bmp))
            {
                #region Generate Point Distribution
                //
                // Here we want to pretend we have thrown dirt at a spot, then let it bounce around. Not very realistic, but it's better than random
                //

                //Generate Random targeting point. (All points will try to converge here first before spreading)
                Point3[] trgt = new Point3[properties.MaxRounds];
                for (int i = 0; i < properties.MaxRounds; i++)
                    trgt[i] = new Point3(random.Next(0, sw), random.Next(0, sh), 0);

                //Move particles around
                var pts = World.Simulate( properties.Particles, trgt, new Point3(0, 0, 0), new Point3(sw, sh, 100 * properties.Scale), this.random);

                //Collect final positions. Ultimiately, we ignore Z position of Point3.

                #endregion
                #region Calculate Polys
                List<Point3> expired = new List<Point3>();              //List of Points that cannot be used again to form a poly - us DoNotReusePoints set to true
                List<List<Point3>> points = new List<List<Point3>>();   //Polygon collection
                for (int i = 0; i < pts.Count; i++)
                {
                    if (expired.Contains(pts[i]))
                        continue;
                    points.Add(pts.Where(x => x != pts[i] && !expired.Contains(x)).Select(x => { x.Z = NotReallyDistanceButShouldDo(pts[i], x); return x; }).Where(x => properties.MaxDistance == -1 || x.Z < (properties.MaxDistance * (1 / properties.Scale))).Take(properties.ClumpSize).ToList());
                    if (properties.DoNotReusePoints)
                        expired.AddRange(points.Last());
                }
                #endregion
                #region Render Polys
                {
                    //Generate Background Graadient
                    foreach (var poly in points.Where(x => x.Count() > properties.MinPoints).Select(y => y.Select(z => new System.Drawing.PointF((float)z.X, (float)z.Y)).ToArray()))
                    {
                        g.FillPolygon(new SolidBrush(Color.FromArgb(properties.Opacity, properties.ColourPalette.Collect()[random.Next(0, 8)])), poly);

                    }
                    l1 = bmp.ToMat();
                }
                #endregion
                #region Apply Gaussian Blur
                {
                    //Ensure SmoothingIntensity is odd
                    if (properties.SmoothingIntensity % 2 == 0)
                        properties.SmoothingIntensity++;
                    if (properties.SmoothingIntensity < 0)
                        properties.SmoothingIntensity = 1;

                    for (int i = 0; i < properties.SmoothingAttempts; i++)
                    {
                        l1 = l1.GaussianBlur(new OpenCvSharp.Size(properties.SmoothingIntensity, properties.SmoothingIntensity), 0);
                    }
                }

                #endregion
                #region Add to epr
                Cv2.Resize(l1, l1, new OpenCvSharp.Size(width, height));
                epr.AddLayer(l1);
                #endregion

            }
        }

        private double NotReallyDistanceButShouldDo(Point3 source, Point3 target)
        {
            //forgot to square root this before bulk generation...
            return Math.Pow(target.X - source.X, 2) + Math.Pow(target.Y - source.Y, 2);
        }

    }
}

