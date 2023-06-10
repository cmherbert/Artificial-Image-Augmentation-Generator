using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.PresetTypes
{
    internal class PresetType_I : IPresetType
    {
        public string Name => "Integer Converter";

        public string TypeID => "I";

        public string ConvertFromValue(object value)
        {
            return value.ToString();
        }

        public object ConvertToValue(string value)
        {
            return int.Parse(value);
        }

        public object RandomiseLarge(Random random, object min, object max)
        {
            if (!(min is int) || !(max is int))
                throw new InvalidOperationException("Invalid type");
            if ((int)min > (int)max)
                throw new Exception("Minimum value is greater than maximum value");
            return Math.Min((int)max, Math.Max((int)min,random.Next((int)min, (int)max + 1)));
        }

        public object RandomiseSmall(Random random, object value, object min, object max, Variance variance)
        {
            if (!(value is int))
                throw new InvalidOperationException("Invalid type");
            if (variance.IsAbsoulte)
            {
                return random.Next((int)value - (int)variance.Value, (int)value + (int)variance.Value);
            }
            else
            {
                if ((int)min > (int)max)
                    throw new Exception("Minimum value is greater than maximum value");
                double var = ((int)max - (int)min) / 2d * variance.Value;
                return Math.Min((int)max, Math.Max((int)min,random.Next((int)value - (int)var, (int)value + (int)var)));
            }
        }
    }
}
