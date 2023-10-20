using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstracleGood : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destroy")
        {
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Player") {
            ScoreManager.score++;
            Debug.Log(ScoreManager.score);
            Destroy(this.gameObject);
        }   
    }
}
