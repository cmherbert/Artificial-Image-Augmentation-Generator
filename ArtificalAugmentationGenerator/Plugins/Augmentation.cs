using ArtificalAugmentationGenerator.Components.Interface.Designer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins
{
    /// <summary>
    /// Abstract Implementation of IAugmentation allowing for selection AugmentationProcessor type
    /// Will default to type as defined by "DefaultProcessor" for compatability with IAugmentation
    /// </summary>
    public abstract class Augmentation : IAugmentation
    {
        private Type _processor = null;
        [Browsable(false)]
        public abstract string Name { get; }
        [Browsable(false)]
        public abstract Type DefaultProcessor { get; }

        /// <summary>
        /// Allows processor to be changed, will default to DefaultProcessor if no change is performed
        /// </summary>
        [TypeConverter(typeof(AugmentationProcessorTypeConverter))]
        [Description("Specifies the processor used to generate augmentations")]
        public Type Processor { get => _processor ?? DefaultProcessor; set => _processor = value; }
    }
}
