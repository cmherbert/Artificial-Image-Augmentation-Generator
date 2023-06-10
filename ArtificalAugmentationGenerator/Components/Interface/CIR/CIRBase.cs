using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Interface.CIR
{
    internal abstract class CIRBase : ICIRResource
    {
        private SGUID _resourceID = SGUID.NewSGUID();
        public SGUID ResourceID => _resourceID;
    }
}
