using ArtificalAugmentationGenerator.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAG_Water
{
    internal class WaterResidueAugmentation : Augmentation
    {
        public override string Name => "Water Residue";

        public override Type DefaultProcessor => typeof(WaterResidueProcessor);

        #region Configurable Properties

        /// <summary>
        /// Number of waterdrops to use to generate the augmentation (Note, water-drops are grouped to create raindrops, so this property does not represent the final number of raindrops present)
        /// </summary>
        [Description("Number of waterdrops to use to generate the augmentation (Note, water-drops are grouped to create raindrops, so this property does not represent the final number of raindrops present)")]
        public int Drops { get; set; } = 300;


        /// <summary>
        /// Specifics the padded region behind each raindrop which is used to create the refraction effect
        /// </summary>
        [Description("Specifics the padded region behind each raindrop which is used to create the refraction effect")]
        public int Refraction { get; set; } = 40;


        /// <summary>
        /// Maximum distance between waterdrops allowed when merging into raindrops
        /// </summary>
        [Description("Maximum distance between waterdrops allowed when merging into raindrops")]
        public int Tolerance { get; set; } = 90;


        #endregion
    }
}
