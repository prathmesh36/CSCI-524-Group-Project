using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TimerTutorial : MonoBehaviour
{
    [SerializeField] private float timeCounter;
    [SerializeField] private float countdownTimer = 5f;
    [SerializeField] private bool isCountdown;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] public GameObject looseText;
    [SerializeField] public GameObject setTimer;

    public GameObject bar;

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
            Debug.Log("WiringTutorialEnded");
            SceneManager.LoadScene("MainTutorial");
        }
        int minutes = Mathf.FloorToInt(isCountdown ? countdownTimer / 60f : timeCounter / 60f);
        int seconds = Mathf.FloorToInt(isCountdown ? countdownTimer - minutes * 60:timeCounter-minutes*60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Start()
    {
        Debug.Log("Timmer Started");
        AnimateBar();
    }



    public void AnimateBar()
    {
        LeanTween.scaleY(bar, 1, countdownTimer);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("MyGame");
    }
}
