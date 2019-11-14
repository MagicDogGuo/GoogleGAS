using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GASDatabaseTest : MonoBehaviour {

    List<string> DataTxt = new List<string>();
    int row=-1;
    int col=-1;

    private void Start()
    {
        //string s = "sdsd,4545,n666";
        //Debug.Log(s.Split(','));
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(900, 50 , 100, 30),"更新"))
        {
            LoadData();
        }

        if (GUI.Button(new Rect(900, 300, 100, 30), "關"))
        {
            Application.Quit();
        }
        for (int i = 0; i < DataTxt.Count; i++)
        {
            GUI.TextField(new Rect(20 + 100 * ((i / 10) ), 50 * ((i % 10) + 1), 100, 30), DataTxt[i]);
        }
    }

    void LoadData()
    {
        StartCoroutine(Upload("getGridNum"));

        for (int i = 1; i < row + 1; i++)
        {
            for (int x = 1; x < col + 1; x++)
            {
                StartCoroutine(Upload("read", i, x));
            }
        }
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
            else if ( mathod == "getGridNum")
            {
                string[] sA;
                sA = www.downloadHandler.text.Split(',');
                row = int.Parse(sA[1]);
                col = int.Parse(sA[3]);
            }
            else
            {
                print(www.downloadHandler.text);
                DataTxt.Add(www.downloadHandler.text);
            }
        }
        
    }


    IEnumerator UploadGroup(int row1, int row2)
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "read");
        form.AddField("name", "Tom");
        form.AddField("hp", "33");
        form.AddField("level", "353");
        form.AddField("row", row1);
        form.AddField("row2", row2);


        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbzbS8P8GhLYp01s1qmerB-y9HzXX1_Skh2lJqTyAyImD9Dy9x2F/exec", form))
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
