using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorClose : MonoBehaviour
{

    public bool closeDoor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enter bathroom");

            closeDoor = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exit bathroom");

            closeDoor = false;
        }
    }

}
