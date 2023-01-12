using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] Objectives objectives;


    private void Awake()
    {
        objectives = GameObject.Find("GameEvents").GetComponent<Objectives>(); 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (objectives.objectiveNum == 0)
            {
                objectives.objectiveNum++;
            }
        }
    }
}
