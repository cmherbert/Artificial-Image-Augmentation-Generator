using ArtificalAugmentationGenerator.Components.Presets;
using ArtificalAugmentationGenerator.Components.Presets.Models;
using ArtificalAugmentationGenerator.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.CMDLine
{
    internal class AugmentationPresetPair
    {
        readonly IAugmentation _effect;
        readonly Preset _preset;
        List<PresetProcessor> _presetProcessors = new List<PresetProcessor>();

        public IAugmentation Effect => _effect;
        public Preset Preset => _preset;
        public IReadOnlyList<PresetProcessor> Processors => _presetProcessors.AsReadOnly();


        public AugmentationPresetPair(AugmentationPackage pack, Preset preset)
        {
            _effect = pack.Item;
            _preset = preset;
            GenerateProcessors();
        }
        public AugmentationPresetPair(Type effect, Preset preset)
        {
            _effect = (Augmentation)Activator.CreateInstance(effect);
            _preset = preset;
            GenerateProcessors();
        }
        public AugmentationPresetPair(AugmentationPresetPair presetPair)
        {
            _effect = (Augmentation)Activator.CreateInstance(presetPair.Effect.GetType());
            _preset = presetPair._preset;
            _presetProcessors = presetPair._presetProcessors;
        }

        private void GenerateProcessors()
        {
            if (!_preset.IsDefault)
            {
                _presetProcessors = _effect.CreateProcessors(_preset);
            }
        }

        public void ApplyLargeRandomisation(IAugmentation effect)
        {
            if (!_preset.IsDefault)
            {
                foreach (var prop in _presetProcessors)
                {
                    prop.ApplyLargeRandomisation(effect);
                }
            }
        }
        public void ApplySmallRandomisation(IAugmentation effect)
        {
            if (!_preset.IsDefault)
            {
                foreach (var prop in _presetProcessors)
                {
                    prop.ApplySmallRandomisation(effect);
                }
            }
        }
    }
}
