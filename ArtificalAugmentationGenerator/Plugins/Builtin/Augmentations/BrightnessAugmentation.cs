using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.Augmentations
{
    internal class BrightnessAugmentation : Augmentation
    {
        private int _brightness = 0;

        public override string Name => "Brightness";

        public override Type DefaultProcessor => typeof(BrightnessProcessor);

        #region Configurable Properties
        /// <summary>
        /// Relative brightness change based on 0, where 0 represents no adjustment from input image.  Max 255, Min -255
        /// </summary>
        [Description("Relative brightness change based on 0, where 0 represents no adjustment from input image. Max 255, Min -255")]
        public virtual int Brightness { get => _brightness; set { if (value > 255 || value < -255) throw new ArgumentOutOfRangeException(); _brightness = value; } }

        #endregion
    }
}
