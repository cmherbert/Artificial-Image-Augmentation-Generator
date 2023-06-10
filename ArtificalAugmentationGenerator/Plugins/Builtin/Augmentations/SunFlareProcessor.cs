using ArtificalAugmentationGenerator.Components;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.Augmentations
{
    internal class SunFlareProcessor : AugmentationProcessor<SunFlareAugmentation>
    {
        public SunFlareProcessor(SunFlareAugmentation effect) : base(effect)
        {

        }


        public override AugmentationProcessorResult ProcessImage(Mat image)
        {
            //! Effect matrix is created on image 20px larger in each direction then cropped to fit input image as final blur leaves border
            int padding = 20;

            if (properties.Size % 2 == 0)
                properties.Size++;

            Mat matSum = Mat.Zeros(properties.Size, properties.Size, MatType.CV_32FC1);

            //Create point map

            for (int rounds = 0; rounds < properties.Rounds; rounds++)
            {
                Point[] edgePoints = new Point[random.Next(properties.MinPoints, properties.MaxPoints + 1)];
                for (int i = 0; i < edgePoints.Length; i++)
                {
                    var ox = random.Next(0, properties.Size);
                    var oy = random.Next(0, properties.Size);
                    edgePoints[i] = new Point(ox, oy);
                }
                Mat mat = Mat.Zeros(properties.Size, properties.Size, MatType.CV_32FC1);
                for (int i = 0; i < edgePoints.Length; i++)
                {
                    mat.Set(edgePoints[i].X, edgePoints[i].Y, mat.Get<float>(edgePoints[i].X, edgePoints[i].Y) + (properties.Intensity));
                }

                mat = mat.BoxFilter(-1, new OpenCvSharp.Size(5, 5), new OpenCvSharp.Point(-1, -1), true, BorderTypes.Constant);
                mat = mat.GaussianBlur(new OpenCvSharp.Size(properties.Size, properties.Size), 0, 0, BorderTypes.Replicate);
                matSum = matSum.Add(mat);
                matSum /= 2;

            }
            matSum.ConvertTo(matSum, MatType.CV_8UC1, 255, 0);


            var matlayer = new Mat(new Size(image.Width + padding * 2, image.Height + padding * 2), MatType.CV_8UC3);
            {
                //Clear Layer
                matlayer.Clear(SunFlareKelvinConverter.FromKelvin(properties.Temperature));
                //Scaleup matSum
                Cv2.Resize(matSum, matSum, matlayer.Size());
                //Merge alpha channel into Layer
                Cv2.Merge(new Mat[] { matlayer, matSum }, matlayer);

                return AugmentationProcessorResult.CreateResult(matlayer.Clone(new Rect(padding, padding, matlayer.Width - padding * 2, matlayer.Height - padding * 2)), true);

            }
        }

        private int Clamp(int x, int min, int max)
        {
            if (x < min)
                return min;
            if (x > max)
                return max;
            return x;
        }
    }
}
