using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RESTController : MonoBehaviour
{
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("http://localhost:5000/Game"));
    }

    //    IEnumerator GetRequest(string uri)
    //    {
    //        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //        {
    //            //// Request and wait for the desired page.
    //            //yield return webRequest.SendWebRequest();

    //            //string[] pages = uri.Split('/');
    //            //int page = pages.Length - 1;

    //            //switch (webRequest.result)
    //            //{
    //            //    case UnityWebRequest.Result.ConnectionError:
    //            //    case UnityWebRequest.Result.DataProcessingError:
    //            //        Debug.LogError(pages[page] + ": Error: " + webRequest.error);
    //            //        break;
    //            //    case UnityWebRequest.Result.ProtocolError:
    //            //        Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
    //            //        break;
    //            //    case UnityWebRequest.Result.Success:
    //            //        var result = webRequest.downloadHandler.data;
    //            //        var json = JsonUtility.FromJson<List<ClassLibrary.PongGame>(webRequest.downloadHandler.text);
    //            //        Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
    //            //        break;
    //            //}
    //        }
    //    }
}
