using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
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
        LeanTween.scaleX(bar, 1, time).setOnComplete(ChangeScene);
        
    }
    public void ChangeScene() {
        SceneManager.LoadScene("MyGame");
    }

}
