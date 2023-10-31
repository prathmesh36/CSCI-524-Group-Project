using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;

    void Start()
    {
        // Disable all pop-ups at the start
        foreach (GameObject popUp in popUps)
        {
            popUp.SetActive(false);
        }

        // Delay the activation of the first pop-up by 10 seconds
        Invoke("ActivateFirstPopUp", 7f);
    }

    // This method is called after the delay
    void ActivateFirstPopUp()
    {
        // Enable the first pop-up
        popUps[popUpIndex].SetActive(true);
    }

    void Update()
    {
        if (popUpIndex < popUps.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Disable the current pop-up
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;

                if (popUpIndex < popUps.Length)
                {
                    // Enable the next pop-up
                    popUps[popUpIndex].SetActive(true);
                }
            }
        }
    }
}
