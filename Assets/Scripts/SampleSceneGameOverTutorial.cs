using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SampleSceneGameOverTutorial : MonoBehaviour
{
    public GameObject gameoverPanel;

    // Start is called before the first frame update
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) {
            LoadYouWonScene();
        }
    }

    // Update is called once per frame
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadYouWonScene()
    {
        PlayerPrefs.SetInt("MagnetPuzzle", -1);
        SceneManager.LoadScene("MainTutorial"); // Load the "YouWon" scene
    }
}
