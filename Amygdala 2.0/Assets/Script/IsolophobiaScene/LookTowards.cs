using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowards : MonoBehaviour
{
    public GameObject Target;
    public bool Constant;
    public bool Active;

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            Active = false;
            transform.LookAt(Target.transform);
        }
        if (Constant)
        {
            transform.LookAt(Target.transform);
        }
    }
}
