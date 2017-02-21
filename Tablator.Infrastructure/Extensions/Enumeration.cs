namespace Tablator.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Reflection;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Méthodes pour aider la manipulation des énumérations
    /// </summary>
    public static class EnumerationExtensions
    {
        /// <summary>
        /// Renvoie le nom à afficher d'une valeur d'énumération
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDisplayName(this Enum value)
        {
            DisplayAttribute attr = value.GetType()
                        .GetTypeInfo()
                        .GetMember(value.ToString())
                        .First()
                        .GetCustomAttributes(false)
                        .OfType<DisplayAttribute>()
                        .LastOrDefault();

            return attr == null ? value.ToString() : attr.Name;
        }

        /// <summary>
        /// Get display's name of an enum value
        /// </summary>
        /// <typeparam name="TEnum">enum's type</typeparam>
        /// <param name="value">enum's value</param>
        /// <returns></returns>
        public static string GetDisplayName<TEnum>(this TEnum value) where TEnum : struct, IConvertible
        {
            if (value.GetAttributeOfType<TEnum, DisplayAttribute>() == null)
                return value.ToString();

            return value.GetAttributeOfType<TEnum, DisplayAttribute>().Name;
        }

        /// <summary>
        /// Get display's description of an enum value
        /// </summary>
        /// <typeparam name="TEnum">enum's type</typeparam>
        /// <param name="value">enum's value</param>
        /// <returns></returns>
        public static string GetDisplayDescription<TEnum>(this TEnum value) where TEnum : struct, IConvertible
        {
            if (value.GetAttributeOfType<TEnum, DisplayAttribute>() == null)
                return value.ToString();

            return value.GetAttributeOfType<TEnum, DisplayAttribute>().Description;
        }

        /// <summary>
        /// Get display's name of an enum value
        /// </summary>
        /// <typeparam name="TEnum">enum's type</typeparam>
        /// <param name="value">enum's value</param>
        /// <returns></returns>
        public static string GetDisplayShortName<TEnum>(this TEnum value) where TEnum : struct, IConvertible
        {
            if (value.GetAttributeOfType<TEnum, DisplayAttribute>() == null)
                return value.ToString();

            return value.GetAttributeOfType<TEnum, DisplayAttribute>().ShortName;
        }

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="value">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        private static T GetAttributeOfType<TEnum, T>(this TEnum value)
            where TEnum : struct, IConvertible
            where T : Attribute => value.GetType()
                        .GetTypeInfo()
                        .GetMember(value.ToString())
                        .First()
                        .GetCustomAttributes(false)
                        .OfType<T>()
                        .LastOrDefault();

        public static T GetValueFromDisplayDescription<T>(string val)
        {
            Type type = typeof(T);

            if (!type.GetTypeInfo().IsEnum)
                throw new InvalidOperationException();

            foreach (FieldInfo field in type.GetTypeInfo().GetFields())
            {
                //var attribute = Attribute.GetCustomAttribute(field,
                //    typeof(DescriptionAttribute)) as DescriptionAttribute;
                //if (attribute != null)
                //{
                //    if (attribute.Description == description)
                //        return (T)field.GetValue(null);
                //}
                //else
                //{
                //    if (field.Name == description)
                //        return (T)field.GetValue(null);
                //}

                Attribute attr = field.GetCustomAttribute(typeof(DisplayAttribute));
                if (attr != null)
                {
                    if (((DisplayAttribute)attr).GetDescription() == val)
                        return (T)field.GetValue(null);
                }
            }

            return default(T);
        }

        public static T GetValueFromDisplayShortName<T>(string val)
        {
            Type type = typeof(T);

            if (!type.GetTypeInfo().IsEnum)
                throw new InvalidOperationException();

            foreach (FieldInfo field in type.GetTypeInfo().GetFields())
            {
                Attribute attr = field.GetCustomAttribute(typeof(DisplayAttribute));
                if (attr != null)
                {
                    if (((DisplayAttribute)attr).GetShortName() == val)
                        return (T)field.GetValue(null);
                }
            }

            return default(T);
        }

        //public static T GetValueFromAttribute<T>(Attribute attr, string val)
        //{
        //    Type type = typeof(T);

        //    if (!type.GetTypeInfo().IsEnum)
        //        throw new InvalidOperationException();

        //    foreach (var field in type.GetFields())
        //    {
        //        var attribute = Attribute.GetCustomAttribute(field,
        //            typeof(DescriptionAttribute)) as DescriptionAttribute;
        //        if (attribute != null)
        //        {
        //            if (attribute.Description == description)
        //                return (T)field.GetValue(null);
        //        }
        //        else
        //        {
        //            if (field.Name == description)
        //                return (T)field.GetValue(null);
        //        }
        //    }

        //    return default(T);
        //}
    }
}