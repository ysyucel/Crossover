using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

public class APIConnectionManager : MonoBehaviour
{
    private bool isConnected = false;

    private IEnumerator CheckInternetConnection (string url, Action<StudentData[]> callback) {

        Debug.Log(url);
        while (!isConnected) {
            if (Application.internetReachability != NetworkReachability.NotReachable) {
                isConnected = true;
                StartCoroutine(GetJSONFromAPI(url, callback));
            } else {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private IEnumerator GetJSONFromAPI (string url, Action<StudentData[]> callback) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success) {
                Debug.LogError("Error: " + webRequest.error);
            } else {
                string jsonString = webRequest.downloadHandler.text;
                // Deserialize the JSON array into a List of MyData objects
                StudentData[] objects = JsonHelper.getJsonArray<StudentData>(jsonString);

                //List<StudentData> dataList1 = JsonUtility.FromJson<ListWrapper<StudentData>>(jsonString).data;
                // Pass the deserialized list to the callback function
                callback(objects);
            }
        }
    }
    public class JsonHelper
    {
        public static T[] getJsonArray<T> (string json) {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }
    // Example method to trigger the API request
    public void MakeAPIRequest (string url, Action<StudentData[]> callback) {
        Debug.Log(url);
        StartCoroutine(CheckInternetConnection(url, callback));
    }
}

