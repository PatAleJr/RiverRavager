using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate;
    public float initialSpawnDelay = 3f;
    private float nextSpawnTime;

    public float minSpawnRate = 0.2f;
    public float spawnRateChange = -0.05f;

    public GameObject Enemy;

    public bool spawning = false;

    public void startPlaying()
    {
        nextSpawnTime = Time.time + initialSpawnDelay;
        spawning = true;
    }

    public void gameOver()
    {
        spawning = false;
    }

    void Update()
    {
        if (!spawning)
            return;

        if (Time.time > nextSpawnTime)
        {
            spawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }

        spawnRate += spawnRateChange * Time.deltaTime;
        if (spawnRate < minSpawnRate)
            spawnRate = minSpawnRate;
    }

    void spawnEnemy()
    {
        Instantiate(Enemy);
    }
}