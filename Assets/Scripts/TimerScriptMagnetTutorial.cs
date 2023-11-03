using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScriptMagnetTutorial : MonoBehaviour
{

    public GameObject bar;
    public int time;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Timmer Started");
        AnimateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateBar() {
        LeanTween.scaleY(bar, 1, time).setOnComplete(ChangeSceneMagnet);
        
    }
    public void ChangeSceneMagnet() {
        PlayerPrefs.SetInt("MagnetPuzzle", 1);
        Debug.Log("Unnati: Setting the magnet Puzzle game player prefs here" + PlayerPrefs.GetInt("MagnetPuzzle"));
        SceneManager.LoadScene("MainTutorial");
    }

}
