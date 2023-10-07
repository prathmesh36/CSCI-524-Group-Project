using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public string FinalGame; // The name of the scene you want to load

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChangeScene")) // Check if the player has touched the object
        {
            SceneManager.LoadScene(FinalGame);
        }
    }
}
