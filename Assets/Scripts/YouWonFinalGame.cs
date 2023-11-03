using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class YouWonFinalGame : MonoBehaviour
{
    public void Start() {
        DateTime currentTime = DateTime.Now;
        long unixTimestamp = (long)(currentTime - new DateTime(1970, 1, 1)).TotalSeconds;
        GameManager.endTime = unixTimestamp;
        Debug.Log("YouWonFinalGame Start Script called and Game Scores Initialized");
        GameManager.fuel = 45;
        GameManager.health = 40;
        GameManager.boolArray = new bool[10];
        GameManager.currentPlanet = 0;
        GameManager.initialLoad = true;
        if (GameManager.endTime != 0 && GameManager.startTime != 0)
        {
            OverallGameAnalytics overallGameAnalytics = new OverallGameAnalytics();
            overallGameAnalytics.timeTakenToFinishGame = (int)(GameManager.endTime - GameManager.startTime);
            overallGameAnalytics.winLossStatus = "Won";
            string jsonOverallGameAnalytics = JsonUtility.ToJson(overallGameAnalytics);
            Analytics.Instance.SaveData("overall-game-data.json", jsonOverallGameAnalytics);
        }
        GameManager.lostCause = "N/A";
    }

  // Start is called before the first frame update
    public void RePlay(){
        SceneManager.LoadScene("MainMenu");
    }
}

