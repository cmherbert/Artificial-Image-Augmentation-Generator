using AAG_Water.Types;
using ArtificalAugmentationGenerator.Plugins;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AAG_Water.CommonRain;

namespace AAG_Water
{
    internal class WaterResidueProcessor : AugmentationProcessor<WaterResidueAugmentation>
    {
        private BlobifyOptions blob_options = new BlobifyOptions()
        {
            Sharpen = false,
        };
        public WaterResidueProcessor(WaterResidueAugmentation effect) : base(effect)
        {
        }

        public override AugmentationProcessorResult ProcessImage(Mat image)
        {
            AugmentationProcessorResult epr = new AugmentationProcessorResult(true);
            int mm = Math.Min(image.Width, image.Height);
            var drops = CommonRain.CreateDrops(random, 0, 0, (int)(image.Width * 1.2), (int)(image.Height * 1.2), properties.Tolerance, properties.Drops, new OpenCvSharp.Size(ScaleSize((int)(mm * 0.9d), true), ScaleSize((int)(mm * 0.9d), false)), new OpenCvSharp.Size(ScaleSize(mm, true), ScaleSize(mm, false)));
            var mat = DrawDropsV2(drops, image);
            using (var reclr = CommonRain.CreateWhiteMat(mat.Rows, mat.Cols))
            {
                reclr.AdjustOpacity(0.1);
                CommonRain.MergeSubMat(mat, reclr, 0, 0);
            }
            epr.AddLayer(mat);

            return epr;
        }

        private int ScaleSize(int x, bool isWidth)
        {
            if (isWidth)
                return (int)Math.Round((6.9917 * Math.Pow(Math.E, 0.0022 * x)));
            else
                return (int)Math.Round((6.697 * Math.Pow(Math.E, 0.0037 * x)));
        }


        private Mat DrawDropsV2(List<RainDrop> drops, Mat image)
        {
            var xx = Mat.FromArray(new float[,] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } });
            int offsetx = (int)(image.Width * 1.2f - image.Width) / 2, offsety = (int)(image.Height * 1.2f - image.Height) / 2;
            Mat finalmat = Mat.Zeros(image.Rows, image.Cols, MatType.CV_8UC4);
            Mat bMat = image.Clone().Resize(new OpenCvSharp.Size((int)(image.Width * 1.2), (int)(image.Height * 1.2)));
            var dropMats = CommonRain.RenderNormalDrops(drops,  blob_options);
            for (int i = 0; i < drops.Count; i++)
            {
                Rect bounds = CommonRain.CreateSafeRefractionRect(bMat, drops[i].Bounds, properties.Refraction);
                using (Mat submat = bMat.Clone(bounds))
                using (Mat reframat = CommonRain.Refract(submat, properties.Tolerance))
                using (Mat reclr = dropMats[i].Clone())
                using (var canny = dropMats[i].Canny(100, 200))
                using (var matcan = dropMats[i].Clone())
                {
                    canny.InflateCanny(matcan);

                    reclr.Edge(new Vec4b(255, 255, 255, 80));

                    reclr.AdjustOpacity(0.2);
                    CommonRain.RecolourNormalMat(dropMats[i], reframat);
                    CommonRain.MergeSubMat(dropMats[i], reclr, 0, 0);
                    int x1 = random.Next(1, 25), x2 = random.Next(1, 25);
                    x1 = x1 % 2 == 0 ? x1 + 1 : x1;
                    x2 = x2 % 2 == 0 ? x2 + 1 : x2;
                    Cv2.GaussianBlur(matcan, matcan, new Size(x1, x2), 1);
                    Cv2.Filter2D(matcan, matcan, -1, xx);
                    matcan.AdjustOpacity(0.1);
                    dropMats[i].MergeSubMat(matcan, 0, 0);
                    dropMats[i].AdjustOpacity(0.3);
                    finalmat.MergeSubMat(dropMats[i], drops[i].Bounds.Y - offsety, drops[i].Bounds.X - offsetx);
                }

            }
            return finalmat;
        }
    }
}
