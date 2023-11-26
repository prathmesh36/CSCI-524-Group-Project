using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartRespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spacecraft" && transform.name.StartsWith("Planet"))
        {
            //Debug.Log(transform.name);
            string[] parts = transform.name.Split(' ');
            if (parts.Length == 2)
            {
                // Attempt to parse the second part as an integer
                if (int.TryParse(parts[1], out int result))
                {
                    GameManager.currentPlanetRespawn = result - 1;
                }
            }
        }

    }
}
