using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins
{
    public abstract class AugmentationProcessor<T> : IAugmentationProcessor where T : IAugmentation
    {
        private static Random Random = new Random(2);
        protected readonly T properties;
        protected readonly Random random;

        public IAugmentation Properties => properties;
        public AugmentationProcessor(T augmentation)
        {
            this.properties = augmentation;
            random = Random;
        }

        public abstract AugmentationProcessorResult ProcessImage(Mat image);
    }
}
