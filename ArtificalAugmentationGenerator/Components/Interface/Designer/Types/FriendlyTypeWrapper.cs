using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Interface.Designer.Types
{
    internal class FriendlyTypeWrapper
    {
        readonly Type _type;
        public Type Type => _type;
        public string Name => _type.Name;
        public FriendlyTypeWrapper(Type type)
        {
            _type = type;
        }

        public static implicit operator Type(FriendlyTypeWrapper wrapper)
        {
            return wrapper._type as Type;
        }

        public static implicit operator FriendlyTypeWrapper(Type type)
        {
            return new FriendlyTypeWrapper(typeof(Type));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
