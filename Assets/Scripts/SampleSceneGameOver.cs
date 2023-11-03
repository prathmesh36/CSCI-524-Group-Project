using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SampleSceneGameOver : MonoBehaviour
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
        SceneManager.LoadScene("YouWonMiniGame"); // Load the "YouWon" scene
    }
}
