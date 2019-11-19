using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
namespace UnkaEditor.Utitlites
{
    public partial class UEditorGUI
    {

        public static void ArrayEditor(SerializedProperty objInfoList, Type objectType, Action<int> ArrayEditorMiddle, Action<int> ArrayEditorTrail, params string[] propertyHeader)
        {
           
            EditorGUILayout.BeginVertical("BOX");
            EditorGUI.indentLevel += 1;
            for (int i = 0; i < objInfoList.arraySize; i++)
            {

                int index = i;
                EditorGUI.indentLevel += 1;
                EditorGUILayout.BeginHorizontal("BOX");
                EditorGUILayout.BeginVertical();


                
                var fields = objectType.GetFields();
                
                for (int a = 0; a < fields.Length; a++)
                {
                    var arrayObject = objInfoList.GetArrayElementAtIndex(i).FindPropertyRelative(fields[a].Name);
                    EditorNameAttribute ena = Attribute.GetCustomAttribute(fields[a], typeof(EditorNameAttribute)) as EditorNameAttribute;
                  
                    if (fields[a].FieldType.IsArray)
                    {
                        
                        ArrayEditor(arrayObject, fields[a].FieldType.GetElementType(), null, null);
                        
                    }
                    else
                    {


                        if (fields[a].FieldType == typeof(Sprite))
                        {
                            EditorGUILayout.Separator();
                            if (ena != null)
                                EditorGUILayout.ObjectField(arrayObject, new GUIContent(ena.Name));
                            else
                                EditorGUILayout.ObjectField(arrayObject);
                        }
                        else if (fields[a].FieldType == typeof(Vector2))
                        {
                            EditorGUILayout.Separator();
                            if (ena != null)
                                EditorGUILayout.PropertyField(arrayObject, new GUIContent(ena.Name));
                            else
                                EditorGUILayout.PropertyField(arrayObject);
                        }
                        else
                        {
                            EditorGUILayout.Separator();
                            if (ena != null)
                                EditorGUILayout.PropertyField(arrayObject, new GUIContent(ena.Name));
                            else
                                EditorGUILayout.PropertyField(arrayObject);
                        }
                    }

                }


                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();

               
                if (GUILayout.Button("刪除"))
                {
                    objInfoList.DeleteArrayElementAtIndex(i);
                }

                if (ArrayEditorMiddle != null)
                    ArrayEditorMiddle(index);


                EditorGUILayout.EndVertical();

                

                if (ArrayEditorTrail != null)
                    ArrayEditorTrail(index);

                EditorGUILayout.EndHorizontal();


                EditorGUILayout.Space();
                EditorGUI.indentLevel -= 1;
            }

            EditorGUI.indentLevel -= 1;

            if (GUILayout.Button("新增欄位"))
            {
                objInfoList.arraySize++;
            }
            //if (GUILayout.Button("刪除全部"))
            //{
            //    objInfoList.ClearArray();
            //}
            EditorGUILayout.EndVertical();
        }


        public static void ClassEditor(SerializedProperty obj, Type objectType, params string[] propertyHeader)
        {
            var fields = objectType.GetFields();

            for (int a = 0; a < fields.Length; a++)
            {

                EditorGUILayout.Separator();
                EditorGUILayout.PropertyField(obj.FindPropertyRelative(fields[a].Name), new GUIContent(propertyHeader[a]));

            }
        }


        public static void ArrayEditor(SerializedProperty objInfoList, Type objectType)
        {

            EditorGUILayout.BeginVertical("BOX");
            EditorGUI.indentLevel += 1;
            for (int i = 0; i < objInfoList.arraySize; i++)
            {

                int index = i;
                EditorGUI.indentLevel += 1;
                EditorGUILayout.BeginHorizontal("BOX");
                EditorGUILayout.BeginVertical();

                var fields = objectType.GetFields();

                for (int a = 0; a < fields.Length; a++)
                {
                    var arrayObject = objInfoList.GetArrayElementAtIndex(i).FindPropertyRelative(fields[a].Name);
                    EditorNameAttribute ena = Attribute.GetCustomAttribute(fields[a], typeof(EditorNameAttribute)) as EditorNameAttribute;
                    if (fields[a].FieldType.IsArray)
                    {

                        ArrayEditor(arrayObject, fields[a].FieldType.GetElementType(), null, null);

                    }
                    else
                    {
                        if (fields[a].FieldType == typeof(Sprite))
                        {
                            EditorGUILayout.Separator();
                            if (ena != null)
                                EditorGUILayout.ObjectField(arrayObject, new GUIContent(ena.Name));
                            else
                                EditorGUILayout.ObjectField(arrayObject);
                        }
                        else if (fields[a].FieldType == typeof(Vector2))
                        {
                            EditorGUILayout.Separator();
                            if (ena != null)
                                EditorGUILayout.PropertyField(arrayObject, new GUIContent(ena.Name));
                            else
                                EditorGUILayout.PropertyField(arrayObject);
                        }
                        else
                        {
                            EditorGUILayout.Separator();
                            if (ena != null)
                                EditorGUILayout.PropertyField(arrayObject, new GUIContent(ena.Name));
                            else
                                EditorGUILayout.PropertyField(arrayObject);
                        }
                    }
                }


                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();


                if (GUILayout.Button("刪除"))
                {
                    objInfoList.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndVertical();


                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUI.indentLevel -= 1;
            }

            EditorGUI.indentLevel -= 1;

            if (GUILayout.Button("新增欄位"))
            {
                objInfoList.arraySize++;
            }
            //if (GUILayout.Button("刪除全部"))
            //{
            //    objInfoList.ClearArray();
            //}
            EditorGUILayout.EndVertical();
        }


        public static void ToogleGroup(ref bool onOff,SerializedProperty property,string ToggleName,string Content)
        {
            onOff = EditorGUILayout.BeginToggleGroup(ToggleName, onOff);
            EditorGUILayout.PropertyField(property, new GUIContent(Content));
            EditorGUILayout.EndToggleGroup();
        }


    }
}