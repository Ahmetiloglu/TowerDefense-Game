using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindHome : MonoBehaviour
{
    public Transform destination;
    private NavMeshAgent ai;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        ai.SetDestination(destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyAI();
    }

    private void DestroyAI()
    {
        if (ai.remainingDistance < 0.5f && ai.hasPath)
        {
            LevelManager.RemoveEnemy();
            ai.ResetPath();
            Destroy(this.gameObject , 0.1f);
        }
    }
    
}
