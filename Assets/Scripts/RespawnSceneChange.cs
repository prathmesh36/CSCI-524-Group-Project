using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RespawnSceneChange : MonoBehaviour
{
    public void Restart(){
        GameManager.fuel=GameManager.lastFuel;
        GameManager.health=GameManager.health-25;
        SceneManager.LoadScene("MyGame");
    }
}
