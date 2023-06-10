using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins.Presets
{
    public class Variance
    {
        bool _isAbs = false;
        double _value = 0;
        public bool IsAbsoulte => _isAbs;
        public double Value => _value;

        public Variance(string variance)
        {
            if (variance.Trim().EndsWith("%"))
            {
                _isAbs = false;
                _value = double.Parse(variance.Trim().Substring(0, variance.Trim().Length - 1)) / 100;
            }
            else
            {
                _isAbs = true;
                _value = double.Parse(variance.Trim());
            }
        }
    }
}
