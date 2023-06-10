using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using ArtificalAugmentationGenerator.Components.Interface.Designer.Types;
using System.Security.AccessControl;
using ArtificalAugmentationGenerator.Plugins;
using System.Globalization;

namespace ArtificalAugmentationGenerator.Components.Interface.Designer
{
    internal class AugmentationProcessorTypeConverter : StringConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(String))
                return true;
            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Type))
                return true;
            return false;

        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is Type)
                return value as Type;
            else if (value is string)
            {
                var type = ContentManager.ProcessorPackages.Where(x => x.Item.Name.Equals(value)).FirstOrDefault();
                if (type != null)
                    return type.Item;
                else
                {

                }
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(Type))
            {

            }
            else if (value is Type v)
                return v.Name;
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            // Return true to indicate that a standard set of values is supported
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // Return true to indicate that the returned values are the only valid options
            return true;
        }
         
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            // Add your logic to return different values based on the parent object type
            if (context?.Instance is IAugmentation parent)
            {
                var processors = ContentManager.ProcessorPackages.Where(x => {
                    if (x.Item.BaseType == null || !x.Item.BaseType.IsGenericType)
                        return false;
                    var t = x.Item.BaseType.GenericTypeArguments[0];
                    var t1 = context.Instance.GetType();
                    return t1.IsEquivalentTo(t);
                    ;
                    }).ToList();
                if (processors.Count == 0)
                    return new StandardValuesCollection(new Type[] { });
                else
                {
                    var items = processors.Select(x => x.Item).ToArray();
                    return new StandardValuesCollection(items);
                }
            }

            return new StandardValuesCollection(new Type[] { });
        }
    }


}
