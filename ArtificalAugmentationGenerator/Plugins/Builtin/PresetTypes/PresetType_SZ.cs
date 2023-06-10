using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Builtin.PresetTypes
{
    internal class PresetType_SZ : IPresetType
    {
        public string Name => "Size Converter";

        public string TypeID => "SZ";

        public string ConvertFromValue(object value)
        {
            if (value is Size)
                return $"{((Size)value).Width},{((Size)value).Height}";
            throw new NotFiniteNumberException();
        }

        public object ConvertToValue(string value)
        {
            if (Regex.IsMatch(value, "^[0-9]*,[0-9]*$"))
            {
                string[] split = value.Split(',');
                return new Size(int.Parse(split[0]), int.Parse(split[1]));
            }
            throw new Exception("Invalid Value");
        }

        public object RandomiseLarge(Random random, object min, object max)
        {
            if (!(min is Size) || !(max is Size))
                throw new InvalidOperationException("Invalid type");
            if (((Size)min).Height > ((Size)max).Height || ((Size)min).Width > ((Size)max).Width)
                throw new Exception("Minimum value is greater than maximum value");
            return new Size(random.Next(((Size)min).Width, ((Size)max).Width + 1), random.Next(((Size)min).Height, ((Size)max).Height + 1));
        }

        public object RandomiseSmall(Random random, object value, object min, object max, Variance variance)
        {
            if (!(value is Size))
                throw new InvalidOperationException("Invalid type");
            if (variance.IsAbsoulte)
            {
                var rw = random.Next(((Size)value).Width - (int)variance.Value, ((Size)value).Width + 1 + (int)variance.Value);
                var rh = random.Next(((Size)value).Height - (int)variance.Value, ((Size)value).Height + 1 + (int)variance.Value);
                return new Size(rw, rh);
            }
            else
            {
                if (((Size)min).Height > ((Size)max).Height || ((Size)min).Width > ((Size)max).Width)
                    throw new Exception("Minimum value is greater than maximum value");
                double varw = (((Size)max).Width - ((Size)min).Width) / 2d * variance.Value;
                double varh = (((Size)max).Height - ((Size)min).Height) / 2d * variance.Value;
                var rw = random.Next(((Size)value).Width - (int)varw, ((Size)value).Width + 1 + (int)varw);
                var rh = random.Next(((Size)value).Height - (int)varh, ((Size)value).Height + 1 + (int)varh);
                return new Size(rw, rh);
            }
        }
    }
}
