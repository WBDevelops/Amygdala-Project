using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTrigger : MonoBehaviour
{
    bool triggered = false;
    public AudioSource shuffle;
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            shuffle.Play();
            triggered = true;
        }
    }
}
