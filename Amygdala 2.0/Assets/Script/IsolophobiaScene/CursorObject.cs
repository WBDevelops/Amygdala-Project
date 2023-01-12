using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Monitor").GetComponent<Monitor>().GetFocusing())
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject TempOB = GameObject.Find("TempOB");
                TempOB.transform.position = hit.point;
                TempOB.transform.localPosition = new Vector3(TempOB.transform.localPosition.x, TempOB.transform.localPosition.y, 0f);
                if (TempOB.transform.localPosition.x > 0.48f)
                {
                    TempOB.transform.localPosition = new Vector3(0.48f, TempOB.transform.localPosition.y, 0f);
                }
                if (TempOB.transform.localPosition.x < -0.48f)
                {
                    TempOB.transform.localPosition = new Vector3(-0.48f, TempOB.transform.localPosition.y, 0f);
                }
                if (TempOB.transform.localPosition.y > 0.48f)
                {
                    TempOB.transform.localPosition = new Vector3(TempOB.transform.localPosition.x, 0.48f, 0f);
                }
                if (TempOB.transform.localPosition.y < -0.48f)
                {
                    TempOB.transform.localPosition = new Vector3(TempOB.transform.localPosition.x, -0.48f, 0f);
                }
                transform.localPosition = new Vector3(TempOB.transform.localPosition.x, TempOB.transform.localPosition.y, 0f);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
