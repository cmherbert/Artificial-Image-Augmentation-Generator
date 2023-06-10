using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.PresetTypes
{
    internal class PresetType_B : IPresetType
    {
        public string Name => "Boolean Converter";

        public string TypeID => "B";

        public string ConvertFromValue(object value)
        {
            return value.ToString();
        }

        public object ConvertToValue(string value)
        {
            return bool.Parse(value);
        }

        public object RandomiseLarge(Random random, object min, object max)
        {
            return random.Next(1) == 1 ? true : false;
        }

        public object RandomiseSmall(Random random, object value, object min, object max, Variance variance)
        {
            return random.Next(1) == 1 ? true : false;
        }
    }
}
