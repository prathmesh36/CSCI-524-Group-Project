using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour
{
    public void Start()
    {
        DateTime currentTime = DateTime.Now;
        long unixTimestamp = (long)(currentTime - new DateTime(1970, 1, 1)).TotalSeconds;
        GameManager.endTime = unixTimestamp;

        if (GameManager.endTime != 0 && GameManager.startTime != 0)
        {
            Analytics analytics = new Analytics();
            OverallGameAnalytics overallGameAnalytics = new OverallGameAnalytics();
            overallGameAnalytics.timeTakenToFinishGame = (int)(GameManager.endTime - GameManager.startTime);
            string jsonOverallGameAnalytics = JsonUtility.ToJson(overallGameAnalytics);
            analytics.SaveData("overall-game-data.json", jsonOverallGameAnalytics);
        }
    }

    public void StartOver(){
        SceneManager.LoadScene("MainMenu");
    }
}
