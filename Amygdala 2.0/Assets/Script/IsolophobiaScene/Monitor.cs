using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{

    private bool Focusing;

    void Start()
    {
        Focusing = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast((GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition)), out hit))
            {
                if (hit.transform.gameObject == this.gameObject && GameObject.Find("Chair").GetComponent<Chair>().GetToggleSit() && !Focusing)
                {
                    GameObject Camera = GameObject.Find("Main Camera");
                    Camera.GetComponent<LookWithMouse>().canLook = false;
                    Camera.transform.LookAt(GameObject.Find("ScreenLook").transform);
                    Focusing = true;
                    GameObject.Find("Main Camera").GetComponent<Interactions>().disableCrosshair = true;
                }
                else if (Focusing)
                {
                    GameObject Camera = GameObject.Find("Main Camera");
                    Camera.GetComponent<LookWithMouse>().canLook = true;
                    Focusing = false;
                    GameObject.Find("Main Camera").GetComponent<Interactions>().disableCrosshair = false;
                }
            }
        }
    }

    public bool GetFocusing()
    {
        return Focusing;
    }
}
