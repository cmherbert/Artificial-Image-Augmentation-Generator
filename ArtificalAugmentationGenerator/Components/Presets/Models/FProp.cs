using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArtificalAugmentationGenerator.Components.Presets.Models
{
    /// <summary>
    /// Fixed Property
    /// </summary>
    internal class FProp : IProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public static FProp CreateFromXML(XmlNode node)
        {
            FProp fProp = new FProp();
            fProp.Name = node.Attributes["name"].Value;
            fProp.Type = node.Attributes["type"].Value;
            fProp.Value = node.Attributes["value"]?.Value ?? "0";

            return fProp;
        }

        public object ApplyPreset(IPresetType processor, bool small)
        {
            return processor.ConvertToValue(Value);
        }

        public XmlNode SaveAsXML(XmlDocument root)
        {
            var xmle = root.CreateElement("FProp");
            xmle.SetAttribute("name", Name);
            xmle.SetAttribute("type", Type);
            xmle.SetAttribute("value", Value);
            return xmle;
        }
    }
}
