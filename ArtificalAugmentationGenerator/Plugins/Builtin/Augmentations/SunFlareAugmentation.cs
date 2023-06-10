using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.Augmentations
{
    internal class SunFlareAugmentation : Augmentation
    {
        public override string Name => "Sun Flare";

        public override Type DefaultProcessor => typeof(SunFlareProcessor);

        #region Configurable Properties
        /// <summary>
        /// Alpha channel intensity, reducing this value makes the augmentation more translucent
        /// </summary>
        [Description("Alpha channel intensity, reducing this value makes the augmentation more translucent")]
        public virtual float Intensity { get; set; } = 65.0f;
        /// <summary>
        /// Maximum number of points that can participate in a single generation attempt, must be more than MinPoints
        /// </summary>
        [Description("Maximum number of points that can participate in a single generation attempt, must be more than MinPoints")]
        public virtual int MaxPoints { get; set; } = 100;
        /// <summary>
        /// Minimum number of points that can participate in a generate attempt, must be less than MaxPoints
        /// </summary>
        [Description("Minimum number of points that can participate in a generate attempt, must be less than MaxPoints")]
        public virtual int MinPoints { get; set; } = 10;
        /// <summary>
        /// Number of generation attempts. Each attempt is merged such that a higher number of attempts will create a stronger augmentation
        /// </summary>
        [Description("Number of generation attempts. Each attempt is merged such that a higher number of attempts will create a stronger augmentation")]
        public virtual int Rounds { get; set; } = 20;
        /// <summary>
        /// Size of matrix used to create augmentation
        /// </summary>
        [Description("Size of matrix used to create augmentation")]
        public virtual int Size { get; set; } = 101;
        /// <summary>
        /// Colour temperature in Kelvins
        /// </summary>
        [Description("Colour temperature in Kelvins")]
        public virtual int Temperature { get; set; } = 6600;
        #endregion
    }
}
