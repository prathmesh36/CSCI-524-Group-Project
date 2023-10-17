using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public float fallSpeed = 5.0f;
    // Start is called before the first frame update
    void Update()
    {
        // Move the asteroid downward
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Check if the asteroid is out of the screen
        if (transform.position.y < -Camera.main.orthographicSize)
        {
            // Destroy the asteroid or respawn it at the top
            Destroy(gameObject);
            // If you want to respawn it:
            // float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
            // transform.position = new Vector3(randomX, Camera.main.orthographicSize, 0);
        }
    }
}
