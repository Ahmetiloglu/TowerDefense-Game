using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float startDelay = 1f;
    public Transform homeLocation;
    public float spawnRate = 0.3f;
    public int maxCount = 10;
    private int count = 0;

    void Start()
    {
        InvokeRepeating("spawner", startDelay, spawnRate);
    }

    void spawner()
    {
        GameObject enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<FindHome>().destination = homeLocation;
        count++;
        if (count >= maxCount)
            CancelInvoke("spawner");

    }
}
