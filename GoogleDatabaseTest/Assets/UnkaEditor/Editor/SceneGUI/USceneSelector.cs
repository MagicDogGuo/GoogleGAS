using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace UnkaEditor.SceneGUI
{

    public class USceneSelector
    {

        private bool IsSelecting = false;
        private GUIStyle guiStyle = new GUIStyle();
        private Action<Vector3> OnSelectedEvent;
        private string Content = "";
        private GameObject cursor;

        private bool isFirstCreate = true;

        ~USceneSelector()
        {
            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
        }

        public USceneSelector()
        {
            
            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
            SceneView.onSceneGUIDelegate += this.OnSceneGUI;

            guiStyle.normal.textColor = Color.red;
            guiStyle.fontSize = 20;
            // style.normal.
        }
        

        public void Select(string content, Action<Vector3> onSelected)
        {
            IsSelecting = true;
            Content = content;
            OnSelectedEvent = onSelected;
        }
        public void Select(string content,GameObject cursorObj, Action<Vector3> onSelected)
        {
            IsSelecting = true;
            Content = content;
            OnSelectedEvent = onSelected;
            cursor = cursorObj;
        }



        void OnSceneGUI(SceneView sceneView)
        {
            // Do your drawing here using Handles.

            if (IsSelecting)
            {
                if (isFirstCreate)
                {

                    isFirstCreate = false;
                }

                Vector3 mousePosition = Event.current.mousePosition;
                Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
                //mousePosition = new Vector3(ray.origin.x, ray.origin.y);
                mousePosition = ray.origin;

                if(cursor!=null)
                    cursor.transform.position = mousePosition;
                

                Handles.color = Color.red;
                Handles.DrawWireCube(mousePosition, Vector3.one);
                
                Handles.Label(mousePosition + new Vector3(-0.65f, 1.5f, 0), Content, guiStyle);
                Handles.color = Color.red;
                Vector2 tmp_pos = mousePosition;
                Handles.Label(mousePosition + new Vector3(-0.5f, 2.5f, 0), tmp_pos.ToString(), guiStyle);
                Event e = Event.current;
                
                switch (e.type)
                {
                    case EventType.MouseDown:

                        if (e.button == 0)
                        {
                          
                            if (OnSelectedEvent != null)
                            {
                                OnSelectedEvent(mousePosition);
                            }
                            IsSelecting = false;
                            isFirstCreate = true;
                            
                            e.Use();
                        }

                        break;
                    case EventType.MouseLeaveWindow:
                      
                        break;
                  
                }

                HandleUtility.Repaint();

            }



            //Handles.BeginGUI();
            //// Do your drawing here using GUI.



            //Handles.EndGUI();
        }


    }
}