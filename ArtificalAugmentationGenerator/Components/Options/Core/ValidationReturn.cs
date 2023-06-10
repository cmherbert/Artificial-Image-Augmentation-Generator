using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Options.Core
{
    public struct ValidationReturn
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ValidationReturn(bool _success, string _message = "OK")
        {
            Success = _success;
            Message = _message;
        }
    }
}
