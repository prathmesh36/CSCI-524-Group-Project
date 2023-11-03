using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [SerializeField] private float timeCounter;
    [SerializeField] private float countdownTimer = 5f;
    [SerializeField] private bool isCountdown;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] public GameObject looseText;
    [SerializeField] public GameObject setTimer;
    private void Update() 
    {
        if(isCountdown && countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
        }else
        {
            timeCounter += Time.deltaTime;
            looseText.SetActive(true);
            setTimer.SetActive(false);
            PlayerPrefs.SetInt("WirePuzzle", -1);
            SceneManager.LoadScene("MyGame");
        }
        int minutes = Mathf.FloorToInt(isCountdown ? countdownTimer / 60f : timeCounter / 60f);
        int seconds = Mathf.FloorToInt(isCountdown ? countdownTimer - minutes * 60:timeCounter-minutes*60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
