using ArtificalAugmentationGenerator.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Interface.Dialogs.Interfaces
{
    internal interface IDialog_EffectSelection : IDialog_Common
    {
        Augmentation SelectedEffect { get; }
    }
}
