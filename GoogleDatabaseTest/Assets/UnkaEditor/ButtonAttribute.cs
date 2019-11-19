
using System;


namespace UnkaEditor.IAttribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        public string label = "";
        public int order = 100;

        public ButtonAttribute() { }
        public ButtonAttribute(string label)
        {
            this.label = label;
        }
        public ButtonAttribute(int order)
        {
            this.order = order;
        }
        public ButtonAttribute(string label, int order)
        {
            this.label = label;
            this.order = order;
        }
    }
}