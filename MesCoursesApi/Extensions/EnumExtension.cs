using System.ComponentModel;

namespace MesCoursesApi.Extensions;

public static class EnumExtension
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault() as DescriptionAttribute;
        return attribute?.Description ?? value.ToString();
    }
    public static T ToEnum<T>(this string value) where T : struct, Enum
    {
        if (Enum.TryParse(typeof(T), value, out var result))
        {
            return (T)result;
        }
        throw new ArgumentException($"Cannot convert {value} to enum of type {typeof(T)}");
    }
}