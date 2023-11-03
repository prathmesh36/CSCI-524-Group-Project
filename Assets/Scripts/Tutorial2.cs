using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial2 : MonoBehaviour
{
public void GoBack(){
        SceneManager.LoadScene("Tutorial1");
    }

public void IAmReady(){
        SceneManager.LoadScene("LevelMenu");
    }

}


