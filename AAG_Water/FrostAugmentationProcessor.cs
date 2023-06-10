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
    internal class FrostAugmentationProcessor : AugmentationProcessor<FrostAugmentation>
    {
        private BlobifyOptions blob_options = new BlobifyOptions()
        {
            Sharpen = true,
            KernelSharpenMatrix = Mat.FromArray(new float[,] { { -2, -1, -2 }, { -1, 13, -1 }, { -2, -1, -2 } })

        };
        public FrostAugmentationProcessor(FrostAugmentation effect) : base(effect)
        {
        }

        public override AugmentationProcessorResult ProcessImage(Mat image)
        {
            AugmentationProcessorResult epr = new AugmentationProcessorResult(true);
            int mm = Math.Min(image.Width, image.Height);
            var drops = CommonRain.CreateDrops(random, 0, 0, (int)(image.Width * 1.2), (int)(image.Height * 1.2), properties.Tolerance, properties.Drops, new OpenCvSharp.Size(ScaleSize((int)(mm * 0.9d), true), ScaleSize((int)(mm * 0.9d), false)), new OpenCvSharp.Size(ScaleSize(mm, true), ScaleSize(mm, false)));
            var mat = DrawDrops(drops, image);
            epr.AddLayer(mat);

            return epr;
        }

        private int ScaleSize(int x, bool isWidth)
        {
            if (isWidth)
                return (int)Math.Round((8.9917 * Math.Pow(Math.E, 0.0022 * x)));
            else
                return (int)Math.Round((12.697 * Math.Pow(Math.E, 0.0037 * x)));
        }


        private Mat DrawDrops(List<RainDrop> drops, Mat image)
        {
            int offsetx = (int)(image.Width * 1.2f - image.Width) / 2, offsety = (int)(image.Height * 1.2f - image.Height) / 2;
            Mat finalmat = Mat.Zeros(image.Rows, image.Cols, MatType.CV_8UC4);
            Mat bMat = image.Clone().Resize(new OpenCvSharp.Size((int)(image.Width * 1.2), (int)(image.Height * 1.2)));
            var dropMats = CommonRain.RenderNormalDrops(drops, blob_options);

            for (int i = 0; i < drops.Count; i++)
            {
                Rect bounds = CommonRain.CreateSafeRefractionRect(bMat, drops[i].Bounds, properties.Tolerance);
                using (Mat submat = bMat.Clone(bounds))
                using (Mat reframat = CommonRain.Refract(submat, properties.Refraction))
                using (Mat reclr = dropMats[i].Clone())
                {
                    reclr.Edge(new Vec4b(255, 255, 255, 80));
                    reclr.AdjustOpacity(0.2);
                    CommonRain.RecolourNormalMat(dropMats[i], reframat);
                    CommonRain.MergeSubMat(dropMats[i], reclr, 0, 0);
                    finalmat.MergeSubMat(dropMats[i], drops[i].Bounds.Y - offsety, drops[i].Bounds.X - offsetx);
                }
            }
            return finalmat;
        }
    }
}
