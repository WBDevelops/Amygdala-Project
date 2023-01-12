using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlonenessAudioManager : MonoBehaviour
{
    private GameObject AmbienceNoises;
    private AudioSource SelectedSound;

    public void StopSound(string Sound)
    {
        SelectedSound = GameObject.Find(Sound).GetComponent<AudioSource>();
        SelectedSound.Stop();
    }
    public void PlaySound(string Sound)
    {
        SelectedSound = GameObject.Find(Sound).GetComponent<AudioSource>();
        SelectedSound.Play();
    }
    public void LoopSound(string Sound)
    {
        SelectedSound = GameObject.Find(Sound).GetComponent <AudioSource>();
        SelectedSound.loop = true;
        SelectedSound.Play();
    }
    public void LoopSound(string Sound, float StartPt, float EndPt)
    {
        SelectedSound = GameObject.Find(Sound).GetComponent<AudioSource>();
        SelectedSound.SetScheduledStartTime(StartPt);
        SelectedSound.SetScheduledEndTime(EndPt);
        SelectedSound.loop = true;
        SelectedSound.Play();
    }
}
