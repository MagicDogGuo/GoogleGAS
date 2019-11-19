using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnkaEditor.Utitlites;

[CustomEditor(typeof(DownloadDatabaseRecord))]
public class GASDatabaseInspector : Editor {

    public static int ErorrTime = 0;

    bool GetItemObjectsFoldout = true;

    private void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GetItemObjectsFoldout = EditorGUILayout.Foldout(GetItemObjectsFoldout, "資料庫下載");

        GUILayout.BeginVertical("Box");

        EditorGUILayout.PropertyField(serializedObject.FindProperty("appID"), new GUIContent("資料表的ID"));

        GUILayout.EndVertical();

        for (int i = 0; i < serializedObject.FindProperty("downloadDatabaseRows").arraySize; i++)
        {
            if (serializedObject.FindProperty("downloadDatabaseRows").GetArrayElementAtIndex(i).FindPropertyRelative("fieldsNames").arraySize < 3)
            {
                serializedObject.FindProperty("downloadDatabaseRows").GetArrayElementAtIndex(i).FindPropertyRelative("fieldsNames").arraySize = 3;
            }
            else if(serializedObject.FindProperty("downloadDatabaseRows").GetArrayElementAtIndex(i).FindPropertyRelative("fieldsNames").arraySize == 5)
            {
                serializedObject.FindProperty("downloadDatabaseRows").GetArrayElementAtIndex(i).FindPropertyRelative("fieldsNames").arraySize = 7;
            }
            else if (serializedObject.FindProperty("downloadDatabaseRows").GetArrayElementAtIndex(i).FindPropertyRelative("fieldsNames").arraySize == 6)
            {
                serializedObject.FindProperty("downloadDatabaseRows").GetArrayElementAtIndex(i).FindPropertyRelative("fieldsNames").arraySize = 4;
            }
            else if (serializedObject.FindProperty("downloadDatabaseRows").GetArrayElementAtIndex(i).FindPropertyRelative("fieldsNames").arraySize >= 7)
            {
                serializedObject.FindProperty("downloadDatabaseRows").GetArrayElementAtIndex(i).FindPropertyRelative("fieldsNames").arraySize = 7;
            }
        }

        if (GetItemObjectsFoldout)
        {
            UEditorGUI.ArrayEditor(serializedObject.FindProperty("downloadDatabaseRows"), typeof(DownloadDatabaseRow));
        }
        serializedObject.ApplyModifiedProperties();
    }
}


public class EorrorWindow : EditorWindow
{
    void Start()
    {
        //視窗彈出時候呼叫
        Debug.Log("My Window　Start");
    }

    void OnGUI()
    {
        GUILayout.Space(50f);

        GUIStyle SectionNameStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter };
        SectionNameStyle.fontSize = 30;
        string s = null;
        s = "===== 超出範圍(1~3個) =====";
        if (GASDatabaseInspector.ErorrTime % 3 == 0)
        {
            s = "不要再按超過了87";
        }
        EditorGUILayout.LabelField(s, SectionNameStyle, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
    }
}
