using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DownloadDatabaseRecord", menuName = "下載GAS資料庫")]
public class DownloadDatabaseRecord : ScriptableObject {

    [SerializeField]
    public string appID;

    [SerializeField]
    public List<DownloadDatabaseRow> downloadDatabaseRows;
}

[System.Serializable]
public class DownloadDatabaseRow
{
    [SerializeField]
    public string sheetName;

    [SerializeField]
    public int col;

    [SerializeField]
    public int row;

    [SerializeField]
    [EditorName("數量必須為3、4、7")]
    public FieldsName[] fieldsNames;


}
[System.Serializable]
public class FieldsName
{
    [EditorName("欄位名稱")]
    public string fields;
}