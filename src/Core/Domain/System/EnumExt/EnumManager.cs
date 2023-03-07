using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System.EnumExt;
public static class EnumManager
{
    /// <summary>
    /// 获取枚举项列表
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="enumObj">枚举项</param>
    /// <param name="markCurrentAsSelected">是否选中当前枚举项</param>
    /// <param name="valuesToExclude">不包含的枚举项</param>
    /// <returns>通用枚举列表</returns>
    public static List<EnumNode> ToSelectList<TEnum>(this TEnum enumObj,
       bool markCurrentAsSelected = true, int[] valuesToExclude = null) where TEnum : struct
    {
        List<EnumNode> values = new List<EnumNode>();

        if (!typeof(TEnum).IsEnum) throw new ArgumentException("An Enumeration type is required.", "enumObj");

        var enums = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                    where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
                    select enumValue;

        foreach (var enumValue in enums)
        {
            var value = new EnumNode
            {
                Id = Convert.ToInt32(enumValue),
                Value = enumValue.ToString(),
                Name = enumValue.ToString()
            };

            FieldInfo field = (typeof(TEnum)).GetField(enumValue.ToString());

            if (field != null)
            {
                var attrs = field.GetCustomAttributes();
                foreach (var attr in attrs)
                {
                    var propertyName = attr.GetType().GetProperty("Name");
                    if (propertyName != null)
                    {
                        value.Name = propertyName.GetValue(attr).ToString();
                    }
                }
            }

            if (markCurrentAsSelected && Convert.ToInt32(enumObj) == value.Id)
            {
                value.Selected = true;
            }

            values.Add(value);
        }

        return values;
    }


    public static string GetEnumDisplayName<T>(T value) where T : Enum
    {
        var fieldName = Enum.GetName(typeof(T), value);
        var displayAttr = typeof(T)
            .GetField(fieldName)
            .GetCustomAttribute<DisplayAttribute>();
        return displayAttr?.Name ?? fieldName;
    }

    public static T GetEnumByDisplayName<T>(string displayName) where T : struct
    {
        Type type = typeof(T);
        string[] names = Enum.GetNames(type);
        string[] array = names;
        foreach (string text in array)
        {
            DisplayAttribute customAttribute = type.GetField(text).GetCustomAttribute<DisplayAttribute>();
            T result;
            if (customAttribute == null)
            {
                if (text == displayName && Enum.TryParse(text, out result))
                {
                    return result;
                }
                continue;
            }
            string a;
            if (customAttribute.ResourceType == null && string.IsNullOrEmpty(customAttribute.Name))
            {
                a = text;
            }
            else if (!(customAttribute.ResourceType != null))
            {
                a = (string.IsNullOrEmpty(customAttribute.Name) ? text : customAttribute.Name);
            }
            else
            {
                ResourceManager resourceManager = new ResourceManager(customAttribute.ResourceType);
                a = resourceManager.GetString(customAttribute.Name);
            }

            if (a == displayName && Enum.TryParse(text, out result))
            {
                return result;
            }
        }
        return (T)Enum.Parse(type, "0");
    }

}

