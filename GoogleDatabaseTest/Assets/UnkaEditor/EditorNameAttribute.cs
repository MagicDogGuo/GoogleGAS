using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class EditorNameAttribute : Attribute {

    public string Name { get; private set; }

    public EditorNameAttribute(string name)
    {
        Name = name;
    }
}
