using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArtificalAugmentationGenerator.Components.Presets.Models
{

    internal class Preset
    {
        bool _default = false;
        public bool IsDefault => _default;
        public string Name { get; set; } = "Unnamed Preset";
        public string Augmentation { get; set; } = "None";
        public List<IProperty> Properties { get; set; } = new List<IProperty>();

        public static Preset CreateDefault(string augmentation)
        {
            var preset = new Preset();
            preset._default = true;
            preset.Name = "Default " + augmentation;
            preset.Augmentation = augmentation;
            return preset;
        }
        public static Preset CreateFromXML(XmlNode node)
        {
            var preset = new Preset();
            preset.Name = node.Attributes["name"].Value;
            preset.Augmentation = node.Attributes["augmentation"].Value;
            foreach (XmlNode cnode in node.ChildNodes)
            {
                if (cnode is XmlElement)
                    switch (cnode.Name.ToLower())
                    {
                        case "vprop":
                            preset.Properties.Add(VProp.CreateFromXML(cnode));
                            break;
                        case "fprop":
                            preset.Properties.Add(FProp.CreateFromXML(cnode));
                            break;

                    }
            }
            return preset;
        }

        internal XmlNode SaveAsXML(XmlDocument root)
        {
            var xmle = root.CreateElement("Preset");
            xmle.SetAttribute("name", Name);
            xmle.SetAttribute("augmentation", Augmentation);
            foreach (var item in Properties)
                xmle.AppendChild(item.SaveAsXML(root));
            return xmle;
        }
    }
}
