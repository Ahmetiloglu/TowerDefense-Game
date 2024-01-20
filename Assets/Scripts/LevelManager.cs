using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject[] spawnPoints;
    private static int totalEnemies;
    

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        foreach (GameObject sp in spawnPoints)
        {
            totalEnemies += sp.GetComponent<Spawn>().maxCount;
        }
        Debug.Log(totalEnemies);
    }

    public static void RemoveEnemy()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            Debug.Log("Level Over");
        }
    }
    

    void Update()
    {
        
    }
}
