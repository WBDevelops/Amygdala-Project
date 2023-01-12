using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private AudioSource ThisAudioSource;

    // Start is called before the first frame update
    public void StartGenerator()
    {
        ThisAudioSource = this.gameObject.GetComponent<AudioSource>();
        ThisAudioSource.Play();
        ThisAudioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ThisAudioSource != null) {
            if (ThisAudioSource.time >= 64.0f)
            {
                ThisAudioSource.time = 8.0f;
            }
        }
    }
}
