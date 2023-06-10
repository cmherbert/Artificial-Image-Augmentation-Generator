using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.PresetTypes
{
    /// <summary>
    /// Double Converter
    /// </summary>
    internal class PresetType_D : IPresetType
    {
        public string Name => "Double Converter";

        public string TypeID => "D";

        public string ConvertFromValue(object value)
        {
            return value.ToString();
        }

        public object ConvertToValue(string value)
        {
            return double.Parse(value);
        }

        public object RandomiseLarge(Random random, object min, object max)
        {
            if (!(min is double) || !(max is double))
                throw new InvalidOperationException("Invalid type");
            if ((double)min > (double)max)
                throw new Exception("Minimum value is greater than maximum value");
            return Math.Min((double)max, Math.Max((double)min,random.NextDouble((double)min, (double)max + 1)));
        }

        public object RandomiseSmall(Random random, object value, object min, object max, Variance variance)
        {
            if (!(value is double))
                throw new InvalidOperationException("Invalid type");
            if (variance.IsAbsoulte)
            {
                return random.NextDouble((double)value - (double)variance.Value, (double)value + (double)variance.Value);
            }
            else
            {
                if ((double)min > (double)max)
                    throw new Exception("Minimum value is greater than maximum value");
                double var = ((double)max - (double)min) / 2d * variance.Value;
                return Math.Min((double)max, Math.Max((double)min,random.NextDouble((double)value - (double)var, (double)value + (double)var)));
            }
        }

        
    }
    internal static class RandomExtenstions
    {
        public static double NextDouble(this Random RandGenerator, double MinValue, double MaxValue)
        {
            return RandGenerator.NextDouble() * (MaxValue - MinValue) + MinValue;
        }
    }
}
