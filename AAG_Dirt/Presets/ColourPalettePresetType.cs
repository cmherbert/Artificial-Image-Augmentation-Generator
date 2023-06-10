using AAG_Dirt.Types;
using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAG_Dirt.Presets
{
    /// <summary>
    /// Dirt Colour Preset Type for Artifical Augmentation Generator preset files
    /// </summary>
    internal class ColourPalettePresetType : IPresetType
    {
        public string Name => "Colour Palette";

        public string TypeID => "DIRTFX::COLOURPALETTE";

        public string ConvertFromValue(object value)
        {
            if (value is DirtyColourPalette)
            {
                return DirtyColourPalette.ConvertToString(((DirtyColourPalette)value));
            }
            else if (value is DirtyColourPalette[])
            {
                return String.Join(",", (value as DirtyColourPalette[]).Select(x => DirtyColourPalette.ConvertToString(x)));
            }
            throw new Exception("Invalid object type");

        }

        public object ConvertToValue(string value)
        {
                if (value.Contains(";"))
                {
                    var str = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    List<DirtyColourPalette> colours = new List<DirtyColourPalette>();
                    foreach (var color in str)
                        colours.Add(DirtyColourPalette.ConvertFromString(color));
                    return colours.Where(x => !(x is null)).ToArray();
                }
                else
                    return DirtyColourPalette.ConvertFromString(value);
            

        }

        public object RandomiseLarge(Random random, object min, object max)
        {
            
            return RandomiseSmall(random, null, min, max, null);
        }

        public object RandomiseSmall(Random random, object value, object min, object max, Variance variance)
        {
            //Max is unused
            //Variance is unused
            if (min is DirtyColourPalette[])
            {
                return (min as DirtyColourPalette[])[random.Next((min as DirtyColourPalette[]).Length)];
            }
            else if (min is DirtyColourPalette)
                return min;
            throw new Exception("Invalid object type");
        }
    }
}
