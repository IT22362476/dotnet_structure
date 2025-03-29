
namespace Inv.Application.Helper
{
     public static class EnumConverter
    {
        public static TEnum? StringToEnum<TEnum>(string enumString) where TEnum : struct, Enum
        {
            if (Enum.TryParse(enumString, true, out TEnum result))
            {
                return result;
            }
            return null;
        }

        public static int? EnumToNumber<TEnum>(TEnum enumValue) where TEnum : struct, Enum
        {
            return Convert.ToInt32(enumValue);
        }

        public static string EnumToString<TEnum>(TEnum enumValue) where TEnum : struct, Enum
        {
            return enumValue.ToString();
        }

        public static TEnum? NumberToEnum<TEnum>(int enumNumber) where TEnum : struct, Enum
        {
            if (Enum.IsDefined(typeof(TEnum), enumNumber))
            {
                return (TEnum)(object)enumNumber;
            }
            return null;
        }
        public static int? ConvertEnumStringToNumber<T>(string enumString) where T : struct, Enum
        {
            if (Enum.TryParse<T>(enumString, true, out var enumValue))
            {
                return Convert.ToInt32(enumValue);
            }
            return null; // Return null if the conversion fails
        }
    }
}
