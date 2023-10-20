using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Spaceship : MonoBehaviour
{
    public float turnSpeed;
    // Start is called before the first frame update
    public int health=100;
    public TMP_Text healthDisplay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * turnSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime);
        
    }
    public void TakeDamage()
    {
        health = health - 10;
        healthDisplay.text = "Health: " + health;
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    public void AddHealth()
    {
        health = health + 10;
        healthDisplay.text = "Health: " + health;
        if (health >= 100)
        {
            SceneManager.LoadScene("MyGame");
        }
    }
 
}
