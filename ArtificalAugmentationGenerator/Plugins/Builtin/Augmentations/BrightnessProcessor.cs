using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.Augmentations
{
    internal class BrightnessProcessor : AugmentationProcessor<BrightnessAugmentation>
    {
        public BrightnessProcessor(BrightnessAugmentation effect) : base(effect)
        {

        }

        public override AugmentationProcessorResult ProcessImage(Mat image)
        {
            image += new Scalar(properties.Brightness, properties.Brightness, properties.Brightness);
            return AugmentationProcessorResult.CreateResult(image, false);
        }
    }
}
