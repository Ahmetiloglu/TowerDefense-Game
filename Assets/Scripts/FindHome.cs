using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FindHome : MonoBehaviour
{
    public Transform destination;
    private NavMeshAgent ai;
    public EnemyDetails enemyDetails;
    private int currentHealth;
    public Slider healthBarPrefab;
    private Slider healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        ai.SetDestination(destination.position);
        ai.speed = enemyDetails.speed;
        currentHealth = enemyDetails.maxHealth;
        
        healthBar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar.maxValue = enemyDetails.maxHealth;
        healthBar.value = enemyDetails.maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        DestroyAI();
        if (healthBar)
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up *1.2f);
        }
    }

    private void DestroyAI()
    {
        if (ai.remainingDistance < 0.5f && ai.hasPath)
        {
            LevelManager.RemoveEnemy();
            ai.ResetPath();
            Destroy(healthBar.gameObject);
            Destroy(this.gameObject , 0.1f);
        }
    }

    public void Hit(int power)
    {
        if (healthBar){
            healthBar.value -= power;
            if (healthBar.value <= 0)
            {
                Destroy(healthBar.gameObject);
                Destroy(this.gameObject);
            }
        }
           
    }
    
}
