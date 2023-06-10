using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Plugins
{
    public interface IAugmentation
    {
        [Browsable(false)]
        string Name { get; }
        [Browsable(false)]
        Type Processor { get; }
    }
}
