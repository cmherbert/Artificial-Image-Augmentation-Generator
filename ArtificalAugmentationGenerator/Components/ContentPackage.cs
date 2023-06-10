using ArtificalAugmentationGenerator.Plugins;
using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components
{
    internal interface IContentPackage<T> : IContentPackage
    {
        T Item { get; }
      
    }

    internal interface IContentPackage
    {
        string Source { get; }
        bool Builtin { get; }
    }
    internal class AugmentationPackage : IContentPackage<IAugmentation>
    {
        IAugmentation _effect;
        string _source;
        bool _builtin = false;

        public IAugmentation Item => _effect;
        public string Source => _source;
        public bool Builtin => _builtin;

        public AugmentationPackage(IAugmentation effect, string source, bool local)
        {
            _effect = effect;
            _source = source;
            _builtin = local;
        }
    }

    internal class PresetTypePackage : IContentPackage<IPresetType>
    {
        IPresetType _effect;
        string _source;
        bool _builtin = false;

        public IPresetType Item => _effect;
        public string Source => _source;
        public bool Builtin => _builtin;

        public PresetTypePackage(IPresetType effect, string source, bool local)
        {
            _effect = effect;
            _source = source;
            _builtin = local;
        }
    }
    internal class ProcessorPackage : IContentPackage<Type>
    {
        Type _processor;
        string _source;
        bool _builtin = false;

        public Type Item => _processor;
        public string Source => _source;
        public bool Builtin => _builtin;

        public ProcessorPackage(Type processor, string source, bool local)
        {
            _processor = processor;
            _source = source;
            _builtin = local;
        }
    }
}
