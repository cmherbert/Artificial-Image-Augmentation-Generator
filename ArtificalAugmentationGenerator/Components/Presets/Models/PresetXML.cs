using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArtificalAugmentationGenerator.Components.Presets.Models
{
    internal class PresetXML
    {
        string _sourcePath = null;

        List<Preset> _presets = new List<Preset>();
        List<PresetXML> _kinder = new List<PresetXML>();

        public List<Preset> Presets => _presets.Union(_kinder.Select(x => x._presets).SelectMany(x => x)).ToList();

        public static PresetXML CreateFromFile(string path)
        {
            PresetXML presetXML = new PresetXML();
            XmlDocument xmld = new XmlDocument();
            xmld.Load(path);
            if (xmld.GetElementsByTagName("Presets").Count == 1)
            {
                foreach (XmlNode preset in xmld["Presets"])
                {
                    if (preset is XmlElement)
                        presetXML._presets.Add(Preset.CreateFromXML(preset));
                }
            }
            else if (xmld.GetElementsByTagName("Links").Count == 1)
            {
                foreach (XmlNode preset in xmld["Links"])
                {
                    string s = "Undefined";
                    try
                    {
                        s = preset.InnerText;
                        if (!File.Exists(s))
                        {
                            if (!File.Exists(Path.Combine(Path.GetDirectoryName(path), s)))
                                throw new FileNotFoundException();
                            else
                                s = Path.Combine(Path.GetDirectoryName(path), s);
                        }
                        var pxml = CreateFromFile(s);
                        presetXML._kinder.Add(pxml);
                    }
                    catch
                    {
                        Console.WriteLine($"Failed to load Preset File {s}");
                    }
                }
            }
            return presetXML;
        }

        public void SaveAsXML()
        {
            XmlDocument xmld = new XmlDocument();
            xmld.CreateXmlDeclaration("1.0", "UTF-8", null);
            var xmle = xmld.CreateElement("Presets");
            foreach (var preset in _presets)
            {
                xmle.AppendChild(xmld.CreateComment(preset.Augmentation));
                xmle.AppendChild(preset.SaveAsXML(xmld));
            }
            foreach (var kind in _kinder)
            {
                kind.SaveAsXML();
            }


        }
    }
}
