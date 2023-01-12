using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public bool ToggleSit;
    void Start()
    {
        ToggleSit = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast((GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition)), out hit))
            {
                if (hit.transform.gameObject == this.gameObject && !ToggleSit && !GameObject.Find("Monitor").GetComponent<Monitor>().GetFocusing())
                {
                    PlayerMovement PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
                    this.gameObject.GetComponent<Collider>().isTrigger = true;
                    PM.canMove = false;
                    PM.canSprint = false;
                    PM.canCrouch = false;
                    PM.horizontalVelocity = new Vector3(0, 0, 0);
                    ToggleSit = true;
                    GameObject Camera = GameObject.Find("Main Camera");
                    Camera.GetComponent<LookWithMouse>().playerBody = GameObject.Find("On Chair").transform;
                    Camera.transform.SetParent(GameObject.Find("On Chair").transform);
                    Camera.transform.position = GameObject.Find("On Chair").transform.position;
                    Camera.transform.LookAt(GameObject.Find("ScreenLook").transform);
                    
                }
                else if (ToggleSit && hit.transform.gameObject != GameObject.Find("Monitor") && hit.transform.gameObject != GameObject.Find("Computer"))
                {
                    PlayerMovement PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
                    PM.canMove = true;
                    PM.canSprint = true;
                    PM.canCrouch = true;
                    ToggleSit = false;
                    GameObject Camera = GameObject.Find("Main Camera");
                    Camera.GetComponent<LookWithMouse>().playerBody = GameObject.Find("Player").transform;
                    Camera.GetComponent<LookWithMouse>().canLook = true;
                    Camera.transform.SetParent(GameObject.Find("Off Chair").transform);
                    Camera.transform.position = GameObject.Find("Off Chair").transform.position;
                }
            }
        }
        if (!ToggleSit)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public bool GetToggleSit()
    {
        return ToggleSit;
    }
}
