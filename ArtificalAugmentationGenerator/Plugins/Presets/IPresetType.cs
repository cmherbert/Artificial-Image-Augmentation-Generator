using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Presets
{
    public interface IPresetType
    {
        string Name { get; }
        string TypeID { get; }

        object ConvertToValue(string value);
        string ConvertFromValue(object value);

        object RandomiseLarge(Random random, object min, object max);
        object RandomiseSmall(Random random, object value, object min, object max, Variance variance);
    }
}
