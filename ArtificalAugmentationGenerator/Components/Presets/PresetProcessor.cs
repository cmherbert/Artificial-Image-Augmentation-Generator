using ArtificalAugmentationGenerator.Components.Presets.Models;
using ArtificalAugmentationGenerator.Plugins;
using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Presets
{
    internal class PresetProcessor
    {
        readonly IPresetType _processor;
        readonly PropertyInfo _target;
        readonly IProperty _preset;

        public PresetProcessor(IPresetType processor, PropertyInfo target, IProperty presetProperty)
        {
            _processor = processor;
            _target = target;
            _preset = presetProperty;
        }

        public void ApplySmallRandomisation(IAugmentation effect)
        {
            _target.SetValue(effect, _preset.ApplyPreset(_processor, true));
        }
        public void ApplyLargeRandomisation(IAugmentation effect)
        {
            _target.SetValue(effect, _preset.ApplyPreset(_processor, false));
        }
    }
}
