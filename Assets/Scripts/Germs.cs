using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Germs : MonoBehaviour
{
    public float speed;
    Spaceship spaceship;
    // Start is called before the first frame update
    void Start()
    {
        spaceship = FindObjectOfType<Spaceship>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, spaceship.transform.position, speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hole")
        {
            int monstersKilled = PlayerPrefs.GetInt(Constants.MONSTERS_KILLED);
            ++monstersKilled;
            PlayerPrefs.SetInt(Constants.MONSTERS_KILLED, monstersKilled);
            //Debug.Log(string.Format("Monsters Consumed so far: {0}", PlayerPrefs.GetInt(Constants.MONSTERS_KILLED)));
            spaceship.AddHealth();
            Destroy(gameObject);
        }
        if (other.tag == "Player" || other.tag=="Ketanu")
        {
            spaceship.TakeDamage();
            speed = 0;
            transform.parent = spaceship.transform;

        }
    }
}
