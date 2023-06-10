using ArtificalAugmentationGenerator.Components.Interface.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Interface.CIR.DialogInterfaces
{
    /// <summary>
    /// Dialog can select image files
    /// </summary>
    internal interface ICap_FileSelection
    {
        event EventHandler SelectionChanged;

        IEnumerable<SFile> ImageNames { get; }
    }
}
