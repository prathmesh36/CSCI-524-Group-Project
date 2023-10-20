using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class Analytics: MonoBehaviour
{
    public static Analytics Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private string url = "https://csci-524-group-project-default-rtdb.firebaseio.com/";

    public void SaveData(string analyticsName, string jsonString)
    {
        Debug.Log("Inside SaveData");
        StartApiCall(analyticsName, jsonString);
    }


    private IEnumerator MakeApiCall(string analyticsName, string jsonString, Action<string> callback)
    {
        string endpoint = url + analyticsName;

        UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("API call failed: " + request.error);
            callback(null);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            callback(responseText);
        }
    }

    public void StartApiCall(string analyticsName, string jsonString)
    {
        StartCoroutine(MakeApiCall(analyticsName, jsonString, HandleApiResponse));
    }

    private void HandleApiResponse(string response)
    {
        if (response != null)
        {
            // Handle the response data here
            Debug.Log("API Response: " + response);
        }
    }

    //private async Task<string> MakeApiCall(string analyticsName, string jsonString)
    //{
    //    using (HttpClient client = new HttpClient())
    //    {
    //        try
    //        {
    //            var request = new HttpRequestMessage(HttpMethod.Post, url + analyticsName);
    //            var content = new StringContent(jsonString, null, "application/json");
    //            request.Content = content;
    //            HttpResponseMessage response = await client.PostAsync(url + analyticsName, content);
    //            response.EnsureSuccessStatusCode();
    //            return await response.Content.ReadAsStringAsync();
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.LogError("API call failed: " + e.Message);
    //            return null;
    //        }
    //    }
    //}


}

