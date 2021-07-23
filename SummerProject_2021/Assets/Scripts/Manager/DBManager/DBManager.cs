/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-23
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    [SerializeField] protected string phpFile;  //php ���� �̸�
    protected string url; //LocalHost URL
    protected string queryResult;
    
/// <summary>
/// �����ͺ��̽� ������ ���� ����
/// </summary>
/// <param name="_phpFile">php ���� �̸�</param>
    protected void Init(string _phpFile)
    {
        url = "http://220.127.167.244:8080/summerproject_2021/";
        phpFile = _phpFile;
    }

    /// <summary>
    /// �����ͺ��̽��� ����
    /// </summary>
    protected virtual IEnumerator ConnectDB()
    {
        using UnityWebRequest www = UnityWebRequest.Get(url + phpFile);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            //TODO: ���� ���� ���н� ���� ����
            yield break;
        }

        queryResult = www.downloadHandler.text;
    }
    
    /// <summary>
    /// ���̺��� �ϳ��� ���� ���� ���� ����
    /// </summary>
    /// <param name="data">�����Ϸ��� �ϴ� ��</param>
    /// <param name="index">���� �̸� ex)"id:"</param>
    /// <param name="seperator">���� ���� �����ϱ� ���� seperator</param>
    /// <returns>�ϳ��� ���� index�� �� �ش��ϴ� ��</returns>
    protected string GetDataValue(string data, string index, string seperator)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(seperator)) value = value.Remove(value.IndexOf(seperator));
        return value;

    }
    
}
