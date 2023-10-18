using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http;
using System.Threading.Tasks;
using System;

public class Analytics
{ 
    private string url = "https://csci-524-group-project-default-rtdb.firebaseio.com/";

    public async void SaveData(string analyticsName, string jsonString)
    {
        Debug.Log("Inside SaveData");
        string response = await MakeApiCall(analyticsName, jsonString);
        Debug.Log("API call completed. Response: " + response);
    }

    private async Task<string> MakeApiCall(string analyticsName, string jsonString)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url + analyticsName);
                var content = new StringContent(jsonString, null, "application/json");
                request.Content = content;
                HttpResponseMessage response = await client.PostAsync(url + analyticsName, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.LogError("API call failed: " + e.Message);
                return null;
            }
        }
    }


}

