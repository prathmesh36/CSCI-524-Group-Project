using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject pipe;
    public GameObject wire;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float timeBetweenSpawn;
    private float spawnTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomValue = Random.Range(0f, 1f);

        GameObject selectedObstacle = null;

        if (randomValue < 0.33f)
        {
            selectedObstacle = obstacle;
        }
        else if (randomValue < 0.66f)
        {
            selectedObstacle = pipe;
        }
        else
        {
            selectedObstacle = wire;
        }

        Instantiate(selectedObstacle, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}
