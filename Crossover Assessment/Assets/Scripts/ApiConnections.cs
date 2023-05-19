using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class APIConnectionManager : MonoBehaviour
{
    private bool isConnected = false;

    private IEnumerator CheckInternetConnection (string url) {
        while (!isConnected) {
            if (Application.internetReachability != NetworkReachability.NotReachable) {
                isConnected = true;
                StartCoroutine(GetJSONFromAPI(url));
            } else {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private IEnumerator GetJSONFromAPI (string url) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success) {
                Debug.LogError("Error: " + webRequest.error);
            } else {
                string jsonString = webRequest.downloadHandler.text;
                // Process the JSON response here
                Debug.Log(jsonString);
            }
        }
    }

    // Example method to trigger the API request
    public void MakeAPIRequest (string url) {
        StartCoroutine(CheckInternetConnection(url));
    }
}