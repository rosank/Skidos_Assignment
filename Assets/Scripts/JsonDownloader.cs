using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class JsonDownloader : MonoBehaviour {
    private const string WEB_DATA_URL = "https://private-5b1d8-sampleapi187.apiary-mock.com/questions";
    private const string LOCAL_DATA_FILE = "offline.json";
    [SerializeField] bool useOfflineIfFailed;

    public static JsonDownloader instance = null;
    public JsonDownloadEvent onJsonDownloaded;
    
    public void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Debug.Assert(!instance);
        }
    }

    public void FetchDataFromServer() {
        StartCoroutine(_FetchDataFromServer(WEB_DATA_URL));
    }

    System.Collections.IEnumerator _FetchDataFromServer(string url) {
        WWW www = new WWW(url);
        yield return www;
        if(!string.IsNullOrEmpty(www.error)) {
            if(string.Compare(url, WEB_DATA_URL) == 0 && useOfflineIfFailed) {
                StartCoroutine(_FetchDataFromServer(Application.streamingAssetsPath + "/" + LOCAL_DATA_FILE));
            } else {
                onJsonDownloaded.Invoke("");
            }
        } else {
            string jsonStr = www.text;
            //a bit modification so that no need to use other json parser
            jsonStr = "{\"questions\":" + jsonStr + "}";
            Debug.Log(jsonStr);
            onJsonDownloaded.Invoke(jsonStr);
        }
    }

    [System.Serializable]
    public class JsonDownloadEvent : UnityEvent<string> {
    }
}


