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
    /// Variable Property
    /// </summary>
    internal class VProp : IProperty
    {
        string _name, _type;
        public string Name { get; set; }
        public string Type { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Variation { get; set; }

        private object _lastValue = null;

        public static VProp CreateFromXML(XmlNode node)
        {
            VProp vProp = new VProp();
            vProp.Name = node.Attributes["name"].Value;
            vProp.Type = node.Attributes["type"].Value;
            vProp.Min = node.Attributes["min"]?.Value ?? "0";
            vProp.Max = node.Attributes["max"]?.Value ?? "1";
            vProp.Variation = node.Attributes["var"]?.Value ?? "100%";

            return vProp;
        }

        public object ApplyPreset(IPresetType processor, bool small)
        {
            if (_lastValue == null || !small)
            {
                _lastValue = processor.RandomiseLarge(PresetApplicator.rand, processor.ConvertToValue(Min), processor.ConvertToValue(Max));
                return _lastValue;
            }
            else
                return processor.RandomiseSmall(PresetApplicator.rand, _lastValue, processor.ConvertToValue(Min), processor.ConvertToValue(Max), new Variance(Variation));
        }

        public XmlNode SaveAsXML(XmlDocument root)
        {
            var xmle = root.CreateElement("VProp");
            xmle.SetAttribute("name", Name);
            xmle.SetAttribute("type", Type);
            xmle.SetAttribute("min", Min);
            xmle.SetAttribute("max", Max);
            xmle.SetAttribute("var", Variation);
            return xmle;
        }
    }
}
