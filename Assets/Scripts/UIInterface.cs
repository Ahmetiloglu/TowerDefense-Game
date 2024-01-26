using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInterface : MonoBehaviour
{
    private GameObject focusObj;
    private GameObject itemPrefab;
    
    public GameObject rocketTurret;
    public GameObject gattlingTurret;
    public GameObject flamerTurret;

    public GameObject turretMenu;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // control for mobile devices
        /*
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)               ********
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);                 ********
            if (!Physics.Raycast(ray, out hit))
                return;
            focusObj = Instantiate(cubeTurret, hit.point, cubeTurret.transform.rotation);
            focusObj.GetComponent<Collider>().enabled = false;
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)          ********
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);                 ********
            if (!Physics.Raycast(ray, out hit))
                return;
            focusObj.transform.position = hit.point + new Vector3(0,1,0);
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)          ********
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);                 ********
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Platform") && hit.normal.Equals(new Vector3(0,1,0)))
            {
                hit.collider.gameObject.tag = "Occupied";
                focusObj.transform.position = new Vector3(hit.collider.gameObject.transform.position.x,
                    focusObj.transform.position.y, hit.collider.gameObject.transform.position.z);
            }
            else
            {
                Destroy(focusObj);
            }

            focusObj = null;
            */
        
            // control for pc 
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())return;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Turret"))
                {
                    turretMenu.transform.position = Input.mousePosition;
                    turretMenu.SetActive(true);
                }
            }
            else if (focusObj && Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                    focusObj.transform.position = hit.point;
            }
            else if (focusObj && Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Platform") && hit.normal.Equals(new Vector3(0,1,0)))
                {
                    hit.collider.gameObject.tag = "Occupied";
                    focusObj.transform.position = new Vector3(hit.collider.gameObject.transform.position.x,
                        focusObj.transform.position.y, hit.collider.gameObject.transform.position.z);
                    
                    focusObj.GetComponent<BoxCollider>().enabled = true;
                    focusObj.GetComponent<SphereCollider>().enabled = true;
                }
                else
                {
                    Destroy(focusObj);
                }

                focusObj = null;
        }
    }

    public void CreateRocket()
    {
        itemPrefab = rocketTurret;
        CreateItemForButton();
    }

    public void CreateGattling()
    {
        itemPrefab = gattlingTurret;
        CreateItemForButton();
    }

    public void CreateFlamer()
    {
        itemPrefab = flamerTurret;
        CreateItemForButton();
    }
    

    public void CreateItemForButton()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit))
            return;
        focusObj = Instantiate(itemPrefab, hit.point, itemPrefab.transform.rotation);
        focusObj.GetComponent<Collider>().enabled = false;
    }

    public void CloseMenu()
    {
        turretMenu.SetActive(false);
    }
    
}
