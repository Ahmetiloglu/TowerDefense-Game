using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterface : MonoBehaviour
{
    private GameObject focusObj;
    public GameObject cubeTurret;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
                return;
            focusObj = Instantiate(cubeTurret, hit.point, cubeTurret.transform.rotation);
            focusObj.GetComponent<Collider>().enabled = false;
        }
        else if (focusObj && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
                return;
            focusObj.transform.position = hit.point + new Vector3(0,1,0);
        }
        else if (focusObj && Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Platform"))
            {
                hit.collider.gameObject.tag = "Occupied";
            }
            else
            {
                Destroy(focusObj);
            }

            focusObj = null;
        }
    }
}
