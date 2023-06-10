using ArtificalAugmentationGenerator.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAG_Water
{
    internal class FocusedRainAugmentation : Augmentation
    {
        public override string Name => "Focused Rain";

        public override Type DefaultProcessor => typeof(FocusedRainProcessor);

        #region Configurable Properties

        /// <summary>
        /// Intensity of blur applied to raindrop refraction
        /// </summary>
        [Description("Intensity of blur applied to raindrop refraction")]
        public int BackgroundBlur { get; set; } = 15;

        /// <summary>
        /// Standard Deviation used to apply blur to raindrop refraction
        /// </summary>
        [Description("Standard Deviation used to apply blur to raindrop refraction")]
        public int BackgroundBlurSD { get; set; } = 3;

        /// <summary>
        /// Number of waterdrops to use to generate the augmentation (Note, water-drops are grouped to create raindrops, so this property does not represent the final number of raindrops present)
        /// </summary>
        [Description("Number of waterdrops to use to generate the augmentation (Note, water-drops are grouped to create raindrops, so this property does not represent the final number of raindrops present)")]
        public int Drops { get; set; } = 150;

        /// <summary>
        /// Intensity of blur applied to raindrop refraction
        /// </summary>
        [Description("Intensity of blur applied to raindrop refraction")]
        public int ForegroundBlur { get; set; } = 1;

        /// <summary>
        /// Specifics the padded region behind each raindrop which is used to create the refraction effect
        /// </summary>
        [Description("Specifics the padded region behind each raindrop which is used to create the refraction effect")]
        public int Refraction { get; set; } = 20;


        /// <summary>
        /// Maximum distance between waterdrops allowed when merging into raindrops
        /// </summary>
        [Description("Maximum distance between waterdrops allowed when merging into raindrops")]
        public int Tolerance { get; set; } = 10;
    

        #endregion
    }
}
