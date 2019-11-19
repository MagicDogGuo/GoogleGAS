using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnkaEditor.Utitlites;


namespace UnkaEditor.InspectorGUI
{

    public partial class UInspector
    {


        public static int EnumPop<T>(int index, string title)
        {
            GUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label(title, GUILayout.Width(100));
            Rect pos = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.popup);
            index = UnkaEditor.Utitlites.UEditorGUI.EnumPopup<T>(pos, index);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
            return index;
        }

        
    }
}