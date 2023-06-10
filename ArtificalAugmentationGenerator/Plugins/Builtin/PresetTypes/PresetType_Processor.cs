using ArtificalAugmentationGenerator.Components;
using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.PresetTypes
{
    internal class PresetType_Processor : IPresetType
    {

        public string Name => "Augmentation Processor Converter";

        public string TypeID => "PX";

        public string ConvertFromValue(object value)
        {
            return value.ToString();
        }

        public object ConvertToValue(string value)
        {
            var type = ContentManager.ProcessorPackages.Where(x => x.Item.Name.Equals(value)).FirstOrDefault();
            if (type != null)
                return type.Item;
            throw new InvalidCastException($"Value {value} is an invalid processor name");
        }

        public object RandomiseLarge(Random random, object min, object max)
        {
            throw new InvalidOperationException("Processor Preset Type does not support randomisation");
        }

        public object RandomiseSmall(Random random, object value, object min, object max, Variance variance)
        {
            throw new InvalidOperationException("Processor Preset Type does not support randomisation");
        }

    }
}
