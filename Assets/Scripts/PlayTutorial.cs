using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTutorial : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("SampleSceneTutorial");
    }
}
