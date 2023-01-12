using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    private AudioSource ThisSource;
    // Start is called before the first frame update
    void Start()
    {
        ThisSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void PlaySpeaker()
    {
        ThisSource.Play();
    }
}
