using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shoot : MonoBehaviour
{
    private FindHome currentTargetCode;
    
    private GameObject currentTarget;
    public GameObject core;
    public GameObject gun;
    public TurretProperties turretProperties;
    private Quaternion coreStartRotation;
    private Quaternion gunStartRotation;

    public AudioSource firingSound;
   
    void Start()
    {
        coreStartRotation = core.transform.rotation;
        gunStartRotation = gun.transform.localRotation;
    }

  
    void Update()
    {
        if (currentTarget != null)
        {
            Vector3 aimAt = new Vector3(currentTarget.transform.position.x, core.transform.position.y,
                currentTarget.transform.position.z);
            //gun.transform.LookAt(currentTarget.transform.position);

            float distanceToTarget = Vector3.Distance(aimAt, gun.transform.position);
            Vector3 realtiveTargetPosition = gun.transform.position + gun.transform.forward * distanceToTarget;
            realtiveTargetPosition = new Vector3(realtiveTargetPosition.x, currentTarget.transform.position.y,
                realtiveTargetPosition.z);
            
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, Quaternion.LookRotation(realtiveTargetPosition - gun.transform.position), Time.deltaTime * turretProperties.turnSpeed);
            core.transform.rotation = Quaternion.Slerp(core.transform.rotation, Quaternion.LookRotation(aimAt - core.transform.position),Time.deltaTime *turretProperties.turnSpeed);
            
            
            Vector3 directionToTarget = currentTarget.transform.position - gun.transform.position;
            if (Vector3.Angle(directionToTarget,gun.transform.forward) < turretProperties.aimingAccuracy)  
            {
                if (Random.Range(0,100) < turretProperties.accuracy)
                {
                    ShootTarget();
                }
            }
        }
        else
        {
            gun.transform.localRotation = Quaternion.Slerp(gun.transform.localRotation, gunStartRotation, Time.deltaTime * turretProperties.turnSpeed);
            
            core.transform.rotation = Quaternion.Slerp(core.transform.rotation, coreStartRotation,Time.deltaTime * turretProperties.turnSpeed);
        }
            
    }
    
    private bool cooldown = true;
    void ShootTarget()
    {
        if (currentTarget && cooldown)
        {
            currentTargetCode.Hit((int)turretProperties.damage);
            firingSound.Play();
            cooldown = false;
            Invoke("CoolDown",turretProperties.reloadTime);
        }    
    }
    void CoolDown()
    {
        cooldown = true;
    }


    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("goob") && currentTarget == null)
        {
            currentTarget = collider.gameObject;
            currentTargetCode = currentTarget.GetComponent<FindHome>();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == currentTarget)
        {
            currentTarget = null;
        }
    }
}
