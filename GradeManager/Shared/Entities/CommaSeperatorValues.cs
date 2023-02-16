using System.ComponentModel;
using System.Globalization;

namespace Shared.Entities
{
    public class CommaSeparatedValues : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var str = value as string;
            if (str == null)
                return null;

            return new List<string>(str.Split(','));
        }
    }
}