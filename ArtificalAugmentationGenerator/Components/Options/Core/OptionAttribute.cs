using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Options.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionAttribute : Attribute
    {
        string _name;
        OptionType _type;
        object _value;
        string _helpText;
        bool _hiddenOption = false;

        public String Name => _name;
        public OptionType OptionType => _type;
        public object DefaultValue => _value;
        public string HelpText => _helpText;
        /// <summary>
        /// Hidden options will not appear in auto generated help information
        /// </summary>
        public bool IsHidden => _hiddenOption;

        public OptionAttribute(string name, OptionType type, string helpText, bool hiddenOption = false)
        {
            _name = name;
            _type = type;
            _value = null;
            _helpText = helpText;
            _hiddenOption = hiddenOption;
        }
        public OptionAttribute(string name, OptionType type, object defaultValue, string helpText, bool hiddenOption = false)
        {
            _name = name;
            _type = type;
            _value = defaultValue;
            _helpText = helpText;
            _hiddenOption = hiddenOption;
        }
    }

}
