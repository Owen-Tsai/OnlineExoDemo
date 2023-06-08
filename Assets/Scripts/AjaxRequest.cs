using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AjaxRequest : MonoBehaviour
{
    private UnityWebRequest www;
    public string URL;

    void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        www = UnityWebRequest.Get("http://localhost:3333/job-list");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.downloadHandler.error);
        }

        Debug.Log(www.downloadHandler.text);
    }
}
