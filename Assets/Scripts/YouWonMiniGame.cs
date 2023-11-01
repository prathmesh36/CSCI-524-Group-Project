using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWonMiniGame : MonoBehaviour
{ 
    // Start is called before the first frame update
    public void ContinueGame(){
        SceneManager.LoadScene("MyGame");
    }

     // Start is called before the first frame update
    public void EndOfTutorial(){
        SceneManager.LoadScene("LevelMenu");
    }
}
