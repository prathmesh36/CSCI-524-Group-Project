using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTutorial : MonoBehaviour
{
    static public MainTutorial Instance;

    public int switchCount;
    public GameObject winText;
    private int onCount = 0;

    private void Awake()
    {
        Instance = this;
    }
    public void SwitchChange(int points) {
        onCount = onCount + points;
        if (onCount == switchCount)
        {
            Debug.Log("MainTutorial.cs");
            winText.SetActive(true);
            PlayerPrefs.SetInt("WirePuzzle", 1);
            SceneManager.LoadScene("YouWonWireMiniGameTutorial");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
