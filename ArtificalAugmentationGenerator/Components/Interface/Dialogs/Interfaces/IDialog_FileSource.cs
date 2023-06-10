using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs.Interfaces
{
    internal interface IDialog_FileSource : IDialog_Common
    {
        event EventHandler SourceFilesChanged;
        IReadOnlyList<string> SourceFiles { get; }
    }
}
