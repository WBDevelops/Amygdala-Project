using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    bool active = true;
    [SerializeField] GameObject flashlight;
    [SerializeField] AudioSource flashlightSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            active = !active;
            flashlightSound.Play();
            flashlight.SetActive(active);
        }
    }
}
