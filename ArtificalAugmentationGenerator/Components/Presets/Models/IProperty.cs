using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArtificalAugmentationGenerator.Components.Presets.Models
{
    internal interface IProperty
    {
        string Name { get; set; }
        string Type { get; set; }

        XmlNode SaveAsXML(XmlDocument root);
        object ApplyPreset(IPresetType processor, bool small);
    }
}
