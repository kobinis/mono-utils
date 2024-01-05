using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XnaUtils
{
    public class ReflectionUtils
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Gets all types in or under a namespace
        /// </summary>        
        public static Type[] GetTypesUnderNamespace(Assembly assembly, string nameSpace, bool includeNested = false)
        {
            return assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.StartsWith(nameSpace) && t.IsNested == includeNested).ToArray();
        }

        public static List<FieldInfo> GetField(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static).ToList();
        }

        public static T CloneWithOverride<T>(T original, T overrideObj) where T : class
        {
            if (original == null || overrideObj == null)
            {
                return overrideObj ?? original;
                //throw new ArgumentNullException("Both arguments must be non-null");
            }

            T result = Activator.CreateInstance<T>();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var originalValue = property.GetValue(original);
                    var overrideValue = property.GetValue(overrideObj);

                    property.SetValue(result, overrideValue ?? originalValue);
                    //if (overrideObj != default)
                       
                    //else
                    //    property.SetValue(result, originalValue);

                }
            }

            return result;
        }

        public static void UpdatePropertiesWithSameName<T1, T2>(T1 target, T2 source)
        {
            var targetProperties = typeof(T1).GetProperties();
            var sourceProperties = typeof(T2).GetProperties();

            foreach (var targetProperty in targetProperties)
            {
                if (targetProperty.CanWrite)
                {
                    var sourceProperty = Array.Find(sourceProperties, p => p.Name == targetProperty.Name && p.CanRead);
                    if (sourceProperty != null && targetProperty.PropertyType == sourceProperty.PropertyType)
                    {
                        var sourceValue = sourceProperty.GetValue(source);
                        targetProperty.SetValue(target, sourceValue);
                    }
                }
            }
        }

    }
}
