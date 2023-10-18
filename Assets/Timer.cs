using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerControl : MonoBehaviour
{
    [SerializeField] private float timeCounter;
    [SerializeField] private float countdownTimer = 120f;
    [SerializeField] private bool isCountdown;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeUpThreshold = -0.01f; // Adjust the threshold as needed

    private void Update()
    {
        if (isCountdown && countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
        }
        else if (!isCountdown)
        {
            timeCounter += Time.deltaTime;
        }

        float currentTimer = isCountdown ? countdownTimer : timeCounter;

        if (currentTimer <= timeUpThreshold)
        {
            timerText.text = "Time is up";
        }
        else
        {
            int minutes = Mathf.FloorToInt(currentTimer / 60f);
            int seconds = Mathf.FloorToInt(currentTimer - minutes * 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
