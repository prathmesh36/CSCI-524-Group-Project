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
        GameManager.fuel = 40;
        GameManager.health = 100;
        GameManager.boolArray = new bool[10];
        GameManager.currentPlanet = 0;
        GameManager.initialLoad = true;

        if (GameManager.endTime != 0 && GameManager.startTime != 0)
        {
            OverallGameAnalytics overallGameAnalytics = new OverallGameAnalytics();
            overallGameAnalytics.timeTakenToFinishGame = (int)(GameManager.endTime - GameManager.startTime);
            overallGameAnalytics.winLossStatus = "Lost-" + GameManager.lostCause;
            string jsonOverallGameAnalytics = JsonUtility.ToJson(overallGameAnalytics);
            Analytics.Instance.SaveData("overall-game-data.json", jsonOverallGameAnalytics);
        }
        GameManager.lostCause = "N/A";

        // Setting First checkpoint behaviour - Landing on Planet/Blackhole/Infinite Space
        CheckpointAnalytics checkpointAnalytics = new CheckpointAnalytics();
        checkpointAnalytics.countBlackhole = PlayerPrefs.GetInt(Constants.COUNT_BLACKHOLE);
        checkpointAnalytics.countInfinity = PlayerPrefs.GetInt(Constants.COUNT_INFINITY);
        checkpointAnalytics.countPlanets = PlayerPrefs.GetInt(Constants.COUNT_PLANETS);
        checkpointAnalytics.countSpacebarClicks = checkpointAnalytics.countBlackhole + checkpointAnalytics.countInfinity
            + checkpointAnalytics.countPlanets;


        string json = JsonUtility.ToJson(checkpointAnalytics);
        Analytics.Instance.SaveData("checkpoint-data.json", json);

        // Reset PlayerPrefs again
        PlayerPrefs.SetInt(Constants.TOTAL_SPACEBAR_CLICKS, 0);
        PlayerPrefs.SetInt(Constants.COUNT_INFINITY, 0);
        PlayerPrefs.SetInt(Constants.COUNT_BLACKHOLE, 0);
        PlayerPrefs.SetInt(Constants.COUNT_PLANETS, 0);

    }

    public void StartOver(){
        SceneManager.LoadScene("MainMenu");
    }
}
