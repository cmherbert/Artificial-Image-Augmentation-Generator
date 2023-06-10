using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins
{
    public interface IAugmentationProcessor
    {
        IAugmentation Properties { get; }
        AugmentationProcessorResult ProcessImage(Mat image);
    }
}
