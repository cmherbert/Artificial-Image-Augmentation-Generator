using ArtificalAugmentationGenerator.Plugins;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAG_Dirt.Sim;

namespace AAG_Dirt
{
    /// <summary>
    /// OpenCV Implementation of Dirt Processor
    /// IT IS STRONGLY RECOMMENDED YOU DO NOT USE THIS VERSION, OpenCV version is slow
    /// </summary>
    internal class DirtOCVProcessor : AugmentationProcessor<DirtAugmentation>
    {
        public DirtOCVProcessor(DirtAugmentation effect) : base(effect)
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
            int sw = (int)(244 * properties.Scale), sh = (int)(244 * properties.Scale);
            Mat l1 = new Mat(sh, sw, MatType.CV_8UC4, new Scalar(0, 0, 0, 0));
            #region Generate Point Distribution
            //
            // Here we want to pretend we have thrown dirt at a spot, then let it bounce around. Not very realistic, but it's better than random
            //

            //Generate Random targeting point. (All points will try to converge here first before spreading)
            Point3[] trgt = new Point3[properties.MaxRounds];
            for (int i = 0; i < properties.MaxRounds; i++)
                trgt[i] = new Point3(random.Next(0, sw), random.Next(0, sh), 0);

            //Move particles around
            var pts = World.Simulate( properties.Particles, trgt, new Point3(0, 0, 0), new Point3(sw, sh, 100), this.random);

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
                var colours = properties.ColourPalette.CollectScalar().Select(x => new Scalar(x.Val0,x.Val1, x.Val2, properties.Opacity)).ToArray();

                foreach (var poly in points.Where(x => x.Count() > properties.MinPoints).Select(y => y.Select(z => new Point(z.X, z.Y)).ToArray()))
                {
                    using (Mat l2 = new Mat(l1.Rows, l1.Cols, MatType.CV_8UC4, Scalar.All(0)))
                    {
                        //Create Polygon
                        Scalar colour = colours[random.Next(0, 8)];
                        Cv2.FillConvexPoly(l2, poly, colour, LineTypes.Link8);

                        //Merge
                        l1.MergeWithAlpha(l2);
                    }
                }

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
            #region cleanup
            pts = null;
            points = null;
            GC.Collect();
            #endregion

        }

        private double NotReallyDistanceButShouldDo(Point3 source, Point3 target)
        {
            //forgot to square root this before bulk generation...
            return Math.Pow(target.X - source.X, 2) + Math.Pow(target.Y - source.Y, 2);
        }

    }
}
