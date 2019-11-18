using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GASDatabaseTest : MonoBehaviour {

    List<string> DataTxt = new List<string>();
    int row =-1;
    int col =-1;

    List<ArrayDataRow> m_database = new List<ArrayDataRow>();


    private void Start()
    {
        //string s = "sdsd,4545,n666";
        //Debug.Log(s.Split(','));
    }

    private void OnGUI()
    {
        for (int i = 0; i < DataTxt.Count; i++)
        {
            GUI.TextField(new Rect(20 + 100 * ((i / 10)), 50 * ((i % 10) + 1), 100, 30), DataTxt[i]);
        }


        if (GUI.Button(new Rect(900, 50 , 100, 30),"更新"))
        {
            LoadData();
        }

        if (GUI.Button(new Rect(900, 300, 100, 30), "存"))
        {
            if (DataTxt.Count == 0)
            {
                Debug.Log("無資料");
            }
            else
            {
                for (int i = 0; i < DataTxt.Count; i++)
                {
                    SaveData(DataTxt[i],"資料名字");
                }
            }
        }

        if (GUI.Button(new Rect(900, 500, 100, 30), "關"))
        {
            Application.Quit();
        }

    }

    void SaveData(string InputString,string FileName)
    {
        JsonData m_jsondata = JsonMapper.ToObject(InputString);

        JsonWriter jsonWriter = new JsonWriter();
        jsonWriter.PrettyPrint = true;
        jsonWriter.IndentValue = 5;
        //把JsonData轉成JsonWriter
        JsonMapper.ToJson(m_jsondata, jsonWriter);

        File.WriteAllText(Application.dataPath + "/Resources/" + FileName+ ".json", jsonWriter.ToString());
        Debug.Log("已儲存");
    }

    string[] FieldArrray = { "Level", "IsPass", "Score" };
    void LoadData()
    {
        //A2
        StartCoroutine(Upload("1NyDlpVforvBe9DeS9c928JYqqlYsftuhdDXGJqrX2wo", "LevelPass", "readGroupToJson_3field", 2,1, FieldArrray[0], FieldArrray[1], FieldArrray[2]));    
    }

    IEnumerator Upload(string mathod,int row1 = 0,int row2 = 0)
    {
        WWWForm form = new WWWForm();
        form.AddField("method", mathod);
        form.AddField("name", "Tom");
        form.AddField("hp", "33");
        form.AddField("level", "353");
        form.AddField("row1", row1);
        form.AddField("row2", row2);

        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbzbS8P8GhLYp01s1qmerB-y9HzXX1_Skh2lJqTyAyImD9Dy9x2F/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else if (mathod == "getGridNum")
            {
                string[] sA;
                sA = www.downloadHandler.text.Split(',');
                row = int.Parse(sA[1]);
                col = int.Parse(sA[3]);

                for (int i = 1; i < row + 1; i++)
                {
                    for (int x = 1; x < col + 1; x++)
                    {
                        StartCoroutine(Upload("read", i, x));
                    }
                }
            }
            else
            {
                print(www.downloadHandler.text);
                DataTxt.Add(www.downloadHandler.text);
            }
        }
    }

    IEnumerator Upload(string appID,string sheetName, string mathod, int row, int col,params string[] fields)
    {
        WWWForm form = new WWWForm();
        form.AddField("appID", appID);
        form.AddField("sheetName", sheetName);

        form.AddField("method", mathod);

        form.AddField("row", row);
        form.AddField("col", col);

        for (int i = 0; i < fields.Length; i++)
        {
            form.AddField("field0"+(i+1), fields[i]);

        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbxEPyvVQa7s4e9c_G_YeZIjteL4T0G4lhPrkI4aOBgQs6FK2UU/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else 
            {
                print(www.downloadHandler.text);
                DataTxt.Add(www.downloadHandler.text);
            }
        }       
    }
}

public class ArrayDataRow {
    public string ID;
    public string Name;
    public string Male;

    public ArrayDataRow(string id,string name,string male)
    {
        this.ID = id;
        this.Name = name;
        this.Male = male;
    }
}
