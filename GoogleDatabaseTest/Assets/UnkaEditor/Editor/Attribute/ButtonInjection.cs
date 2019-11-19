
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace UnkaEditor.IAttribute
{

    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonInjection : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawButtons();
        }

        private void DrawButtons()
        {
            // Read ButtonAttribute.
            var buttonMethods = AttributeReader.ReadMethods<ButtonAttribute>(target.GetType());
            int n = buttonMethods.Count();
            if (n > 0)
            {
                // Order it.
                buttonMethods = buttonMethods.OrderBy(m =>
                {
                    ButtonAttribute attr = AttributeReader.GetMethodAttribute<ButtonAttribute>(m);
                    return attr.order;
                });

                // Draw.
                for (int i = 0; i < n; i++)
                {
                    MethodInfo method = buttonMethods.ElementAt(i);
                    ButtonAttribute attr = AttributeReader.GetMethodAttribute<ButtonAttribute>(method);

                    string label = attr.label;
                    if (string.IsNullOrEmpty(attr.label))
                        label = Regex.Replace(method.Name, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

                    if (GUILayout.Button(label))
                    {
                        method.Invoke(target, null);
                    }
                }
            }
        }

    }
}