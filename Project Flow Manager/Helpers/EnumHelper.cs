using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Project_Flow_Manager.Helpers
{
    public static class EnumHelper<T>
        where T : struct, Enum
    {
        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(x => x.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        private static IList<T> GetValues(Enum value)
        {
            var values = new List<T>();
            foreach (FieldInfo info in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                values.Add((T)Enum.Parse(value.GetType(), info.Name, false));
            }
            return values;
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            var resourceKeyProperty = resourceManagerProvider.GetProperty(resourceKey,
                BindingFlags.Static | BindingFlags.Public, null, typeof(string),
                new Type[0], null);

            if (resourceKeyProperty != null)
            {
                return (string)resourceKeyProperty.GetMethod.Invoke(null, null);
            }

            return resourceKey;
        }
    }
}
