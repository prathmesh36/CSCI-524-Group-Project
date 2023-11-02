using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayButton(){
        Debug.Log("Inside PlayButton!");

        // Setting PlayerPrefs at the start of the game
        // 1. First checkpoint behaviour - Landing on Planet/Blackhole/Infinite Space
        PlayerPrefs.SetInt(Constants.TOTAL_SPACEBAR_CLICKS, 0);
        PlayerPrefs.SetInt(Constants.COUNT_INFINITY, 0);
        PlayerPrefs.SetInt(Constants.COUNT_BLACKHOLE, 0);
        PlayerPrefs.SetInt(Constants.COUNT_PLANETS, 0);

        // 2. First Mini Game - Space Monsters
        PlayerPrefs.SetInt(Constants.MONSTERS_KILLED, 0);
        PlayerPrefs.SetInt(Constants.TOTAL_MONSTERS, 0);

        // 3. Second Mini Game - Pipe Puzzle
        PlayerPrefs.SetInt(Constants.TOTAL_MOUSE_CLICKS, 0);
        PlayerPrefs.SetInt(Constants.TOTAL_MINES_CLICKS, 0);

        // 4. Trend for Fuel over time - Analysing user behaviour
        PlayerPrefs.SetInt(Constants.TOTAL_FUEL, 100);
        PlayerPrefs.SetInt(Constants.BLACKHOLE_IMPACT , 0); 
        PlayerPrefs.SetInt(Constants.ASTEROID_IMPACT , 0); 
        PlayerPrefs.SetInt(Constants.PLANET_IMPACT , 0); 
        PlayerPrefs.SetInt(Constants.INFINITE_SPACE_IMPACT , 0); 
        
        //Debug.Log("Inside PlayButton!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
