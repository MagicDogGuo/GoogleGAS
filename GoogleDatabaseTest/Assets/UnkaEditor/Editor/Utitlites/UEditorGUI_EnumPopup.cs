using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace UnkaEditor.Utitlites
{
    public partial class UEditorGUI
    {
        // Cache of enum values.
        private static Dictionary<Type, int[]> s_EnumValueCache = new Dictionary<Type, int[]>();
        // Cache of enum descriptions/nicified values.
        private static Dictionary<Type, string[]> s_EnumDescriptionCache = new Dictionary<Type, string[]>();
        public static int[] GetEnumValues<T>()
        {
            if (!s_EnumValueCache.ContainsKey(typeof(T)))
                s_EnumValueCache[typeof(T)] = Enum.GetValues(typeof(T)).Cast<int>().ToArray();
            return s_EnumValueCache[typeof(T)];
        }
        public static string[] GetEnumDescriptions<T>()
        {
            if (!s_EnumDescriptionCache.ContainsKey(typeof(T)))
            {
                var list = new List<string>();
                foreach (var value in Enum.GetValues(typeof(T)))
                {
                    var attribute = typeof(T).GetMember(value.ToString())[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                    list.Add(attribute != null ? attribute.Description : ObjectNames.NicifyVariableName(value.ToString()));
                }
                s_EnumDescriptionCache[typeof(T)] = list.ToArray();
            }
            return s_EnumDescriptionCache[typeof(T)];
        }
        public static int EnumPopup<T>(Rect position, int value)
        {
            var values = GetEnumValues<T>();
            int selectedIndex = EditorGUI.Popup(position, Array.IndexOf(values, value), GetEnumDescriptions<T>());
            return (selectedIndex >= 0 && selectedIndex < values.Length) ? values[selectedIndex] : value;
        }
    }
}