using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Interface.Struct
{
    internal struct SFile
    {
        string FilePath {get;set;}
        string FileName => Path.GetFileName(FilePath);
    }
}
