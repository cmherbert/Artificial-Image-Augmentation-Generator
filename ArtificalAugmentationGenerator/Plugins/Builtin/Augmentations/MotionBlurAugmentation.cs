using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.Augmentations
{
    internal class MotionBlurAugmentation : Augmentation
    {
        public override string Name => "Motion Blur";

        public override Type DefaultProcessor => typeof(MotionBlurProcessor);

        private int _direction = 0;

        private int _size = 5;

        #region Configurable Properties

        /// <summary>
        /// Angle of motion blur, measured in degrees from centre right of image
        /// </summary>
        [DefaultValue(0)]
        [Description("Angle of motion blur, measured in degrees from centre right of image")]
        public int Direction { get => _direction; set { _direction = value; } }

        /// <summary>
        /// Specifies the strength of the blur applied where strength is proportional to the value of Size. Value must be odd, even numbers are incremented by one.
        /// </summary>
        [DefaultValue(5)]
        [Description("Specifies the strength of the blur applied where strength is proportional to the value of Size. Value must be odd, even numbers are incremented by one.")]
        public int Size
        {
            get => _size;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Size Invalid, must be greater than 0", "value");
                _size = value;
            }
        }

        #endregion
    }
}
