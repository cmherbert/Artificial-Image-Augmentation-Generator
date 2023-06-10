using ArtificalAugmentationGenerator.Components.Presets.Models;
using ArtificalAugmentationGenerator.Plugins;
using ArtificalAugmentationGenerator.Plugins.Builtin.PresetTypes;
using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Presets
{
    internal static class PresetApplicator
    {
        public static Random rand = new Random();
        public static List<PresetProcessor> CreateProcessors<T>(this T effect, Preset preset) where T : IAugmentation
        {
            List<PresetProcessor> processorList = new List<PresetProcessor>();
            //Apply Preset stuff
            var Properties = effect.GetType().GetProperties().Where(x => preset.Properties.Find(y => y.Name.ToUpper().Equals(x.Name.ToUpper())) != null).ToList();

            foreach (var property in Properties)
            {
                var presetProp = preset.Properties.Find(y => y.Name.ToUpper().Equals(property.Name.ToUpper()));
                processorList.Add(new PresetProcessor(CreateProcessor(presetProp.Type), property, presetProp));

            }
            return processorList;
            
        }

        private static IPresetType CreateProcessor(string type) {

            var processor = ContentManager.PresetTypes.FirstOrDefault(x => x.Item.TypeID.Equals(type, StringComparison.InvariantCultureIgnoreCase));
            if (processor == null)
                throw new NotImplementedException($"Preset processor type of {type} has not been implemented");
            else
                return (IPresetType)Activator.CreateInstance(processor.Item.GetType());
            
        }
    }
}
