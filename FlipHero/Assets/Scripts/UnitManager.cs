using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour
{
    public Transform[] spawnPos;
    public GameObject[] Obstacles;

    public GameObject coin;

    [SerializeField] float waitBetweenSpawn = 5f;

    [SerializeField] float maxWaitBetweenTime = 1f;

    void Start()
    {
        PlayerBehaviour.onPlayerDeath += playerDied;
        InvokeRepeating("ObstacleSpawner", 0.5f, waitBetweenSpawn);
        InvokeRepeating("CoinSpawner", 0.5f, 1.5f);
        GameManager.onSpeedChange += ChangeSpawnRate;
    }

    void ObstacleSpawner()
    {
        Instantiate(Obstacles[Random.Range(0, Obstacles.Length)],spawnPos[Random.Range(0, spawnPos.Length - 1)]);
    }

    void CoinSpawner()
    {
        Instantiate(coin, spawnPos[spawnPos.Length - 1]);
    }

    void ChangeSpawnRate(float newSpeed)
    {
        if (waitBetweenSpawn > maxWaitBetweenTime)
        {
            CancelInvoke("ObstacleSpawner");
            waitBetweenSpawn -= 0.20f;
            InvokeRepeating("ObstacleSpawner", 1f, waitBetweenSpawn);
        }
    }

    void playerDied()
    {
        CancelInvoke("ObstacleSpawner");
        CancelInvoke("CoinSpawner");
    }
}
