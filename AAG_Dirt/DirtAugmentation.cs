using AAG_Dirt.Types;
using ArtificalAugmentationGenerator.Plugins;
using System;
using System.ComponentModel;

namespace AAG_Dirt
{
    /// <summary>
    /// Configurable Properties for the Dirt Augmentation
    /// </summary>
    public class DirtAugmentation : Augmentation
    {
        int _opacity = 64;

        #region Configurable Properties

        /// <summary>
        /// Specifies the maximum number of particles that can be used to create a dirt clump polygon. Dirt clumps are combined to make dirt patches. 
        /// </summary>
        [Description("Specifies the maximum number of particles that can be used to create a dirt clump polygon. Dirt clumps are combined to make dirt patches. ")]
        public int ClumpSize { get; set; } = 40;

        /// <summary>
        /// Provides a collection of eight colours that are used to colour dirt clumps. See Class for known colours.
        /// </summary>
        [Description("Provides a collection of eight colours that are used to colour dirt clumps. See DropDown for known colours.")]
        public DirtyColourPalette ColourPalette { get; set; } = KnownDirtyColourPalettes.Palette1;

        /// <summary>
        /// If false, ensures that each particle is used in a maximum of one dirt clump polygon. If true, particles can be reused in multiple dirt clumps. 
        /// </summary>
        [Description("If false, ensures that each particle is used in a maximum of one dirt clump polygon. If true, particles can be reused in multiple dirt clumps. ")]
        public bool DoNotReusePoints { get; set; } = false;

        /// <summary>
        /// Maximum distance to search for nearby particles when creating dirt clump polygons.
        /// </summary>
        [Description("Maximum distance to search for nearby particles when creating dirt clump polygons. ")]
        public int MaxDistance { get; set; } = 400;

        /// <summary>
        /// Specifies the maximum number of times a particle can be “moved” when distributing the particles.
        /// </summary>
        [Description("Specifies the maximum number of times a particle can be “moved” when distributing the particles.")]
        public int MaxRounds { get; set; } = 200;

        /// <summary>
        /// Specifies the minimum number of points a calculated dirt clump polygon must have to be considered a valid dirt clump. Invalid dirt clumps are not used for dirt patch creation.
        /// </summary>
        [Description("Specifies the minimum number of points a calculated dirt clump polygon must have to be considered a valid dirt clump. Invalid dirt clumps are not used for dirt patch creation.")]
        public int MinPoints { get; set; } = 6;

        /// <summary>
        /// Specifies the opacity of each dirt clump polygon when rendered
        /// </summary>
        [Description("Specifies the opacity of each dirt clump polygon when rendered")]
        public int Opacity { get => _opacity; set { if (value > 255 || value < 0) throw new Exception("Not valid"); else _opacity = value; } }

        /// <summary>
        /// Specifies the number of random points (particles) that are used each round to creation the individual dirt clumps in each dirt patch.
        /// </summary>
        [Description("Specifies the number of random points (particles) that are used each round to creation the individual dirt clumps in each dirt patch.")]
        public int Particles { get; set; } = 1000;

        /// <summary>
        /// Specifies the number of dirt patches to generate on the image
        /// </summary>
        [Description("Specifies the number of dirt patches to generate on the image")]
        public int DirtPatches { get; set; } = 1;

        /// <summary>
        /// Specifies the floating point multiplier used to resize the final augmentation
        /// </summary>
        [Description("Specifies the floating point multiplier used to resize the final augmentation")]
        public double Scale { get; set; } = 1.0f;

        /// <summary>
        /// Number of smoothing operations to apply. A higher number of blurs should smoothen the edges of dirt patches. Works in conjunction with SmoothingIntensity.
        /// </summary>
        [Description("Number of smoothing operations to apply. A higher number of blurs should smoothen the edges of dirt patches. Works in conjunction with SmoothingIntensity.")]
        public int SmoothingAttempts { get; set; } = 12;

        /// <summary>
        /// Strength of blur used to smooth dirt patches on each smoothing attempt. Works in conjunction with SmoothingAttempts.
        /// </summary>
        [Description("Strength of blur used to smooth dirt patches on each smoothing attempt. Works in conjunction with SmoothingAttempts.")]
        public int SmoothingIntensity { get; set; } = 3;
        #endregion

        public override string Name => "Dirt";

        [Description("Specifies the processor used to generate augmentations. As standard, the dirt augmentation library supplies two processors:\n\nDirtGDIProcessor: Uses GDI+ to render polygons. May not operate as expected outside of Windows. However, is faster than OpenCV.\n\nDirtOCVProcessor: Uses OpenCV. Much slower due to inefficencies implementing composting (in this program, OpenCV does not perform composting by default")]
        public override Type DefaultProcessor => typeof(DirtGDIProcessor);
    }
}
