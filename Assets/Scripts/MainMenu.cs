using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayButton(){
        Debug.Log("Inside PlayButton!");

        // Setting PlayerPrefs at the start of the game
        PlayerPrefs.SetInt(Constants.TOTAL_SPACEBAR_CLICKS, 0);
        PlayerPrefs.SetInt(Constants.COUNT_INFINITY, 0);
        PlayerPrefs.SetInt(Constants.COUNT_BLACKHOLE, 0);
        PlayerPrefs.SetInt(Constants.COUNT_PLANETS, 0);

        PlayerPrefs.SetInt(Constants.MONSTERS_KILLED, 0);
        PlayerPrefs.SetInt(Constants.TOTAL_MONSTERS, 0);

        PlayerPrefs.SetInt(Constants.TOTAL_MOUSE_CLICKS, 0);
        PlayerPrefs.SetInt(Constants.TOTAL_MINES_CLICKS, 0);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
