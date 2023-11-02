using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetection : MonoBehaviour
{
    GameManager gameManager;
    public GameObject meteorPrefab;

    private int hits = 0;
    private int maxHits = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meteor"))
        {
            // Remove the meteor object
            Destroy(other.gameObject);

            // Increase the hit count
            hits++;

            if (hits >= maxHits)
            {
                // Remove the shield sphere when it has taken enough hits
                Destroy(gameObject);
                gameManager.updateHealth(-10);
            }
        }
    }
}

