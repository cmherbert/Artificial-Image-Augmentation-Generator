using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.Augmentations
{
    internal class MotionBlurProcessor : AugmentationProcessor<MotionBlurAugmentation>
    {
        public MotionBlurProcessor(MotionBlurAugmentation effect) : base(effect)
        {
        }

        public override AugmentationProcessorResult ProcessImage(Mat image)
        {
            //Scale 
            int s = (int)((image.Width + image.Height) / 2d / 244d);
            //Convert to 32FC3
            image.ConvertTo(image, MatType.CV_32FC3, 1, 0);
            //Generate Directional Mat
            Mat k = GenerateKernel(properties.Size * s);
            //Resize Image
            var sz = image.Size();
            //Apply Kernel
            Cv2.Filter2D(image, image, -1, k);
            //Convert back to 8UC3
            image.ConvertTo(image, MatType.CV_8UC3);
            k.Dispose();
            return AugmentationProcessorResult.CreateResult(image, false);
        }
        private Mat GenerateKernel(int size)
        {
            Mat k = Mat.Zeros(size, size, MatType.CV_32FC1);
            for (int i = 0; i <= size / 2; i++)
            {
                k.Set<float>(i, i, 1);
            }
            Point2f rotationCenter = new Point2f(k.Width / 2, k.Height / 2);
            Mat kr = Cv2.GetRotationMatrix2D(rotationCenter, (properties.Direction - 135), 1);

            Cv2.WarpAffine(k, k, kr, k.Size());

            Cv2.Normalize(k, k, 1, 0, NormTypes.L1);

            return k;

        }

    }
}
