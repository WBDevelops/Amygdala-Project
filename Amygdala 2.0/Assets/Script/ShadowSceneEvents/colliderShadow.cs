using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderShadow : MonoBehaviour
{
    public GameObject shadowObj;
    public AudioSource sound;
    public GameObject shadowMan;
    bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            shadowObj.SetActive(true);
            sound.Play();
            shadowMan.SetActive(true);
            triggered = true;
        }
        
    }

    private void Update()
    {
        if (!sound.isPlaying)
        {
            shadowObj.SetActive(false);
        }
    }
}
