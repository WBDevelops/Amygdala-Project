using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingAudio : MonoBehaviour
{
    private AudioSource BeingsAudio;
    private bool Looping;
    private float timeBetween;
    private float wait;
    // Start is called before the first frame update
    void Start()
    {
        Looping = false;
        BeingsAudio = GameObject.Find("Being's Audio").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Looping)
        {
            wait += Time.deltaTime;
            if(wait >= (timeBetween + BeingsAudio.clip.length))
            {
                wait = 0;
                BeingsAudio.Play();
            }
        }
    }

    public IEnumerator Play(string gameObject_name, float extra_wait_time)
    {
        BeingsAudio.clip = GameObject.Find(gameObject_name).GetComponent<AudioSource>().clip;
        BeingsAudio.Play();
        yield return new WaitForSeconds(BeingsAudio.clip.length + extra_wait_time);
    }
    public void PlayLoop(string gameObject_name, float extra_wait_time)
    {
        BeingsAudio.clip = GameObject.Find(gameObject_name).GetComponent<AudioSource>().clip;
        Looping = true;
    }
    public void StopLoop()
    {
        Looping = false;
    }

    public void StartBootingUp()
    {
        StartCoroutine(BootUp());
    }

    public IEnumerator BootUp()
    {
        yield return StartCoroutine(Play("Audio operational", 0.5f));
        yield return StartCoroutine(Play("Calibrating", 1.0f));
        yield return StartCoroutine(Play("Successful", 0.5f));
        yield return StartCoroutine(Play("Voice Function tests comencing", 0.5f));
        yield return StartCoroutine(Play("Function One", 0.5f));
        yield return StartCoroutine(Play("Sympathetic Response", 1.0f));
        yield return StartCoroutine(Play("Human Cry", 0.5f));
        yield return StartCoroutine(Play("Test Succesful", 0.5f));
        yield return StartCoroutine(Play("Function Two", 0.5f));
        yield return StartCoroutine(Play("Fear", 1.0f));
        yield return StartCoroutine(Play("Fear Sound", 0.5f));
        yield return StartCoroutine(Play("Test Succesful", 0.5f));
        yield return StartCoroutine(Play("Function tests Complete", 0.5f));
        yield return StartCoroutine(Play("Movement Enabled", 0.5f));
        this.gameObject.GetComponent<BeingMovement>().StartWalking = true;
        yield return StartCoroutine(Play("Test Failed", 0.5f));
        PlayLoop("I cannot see", 15.0f);
    }
    public IEnumerator ICanHearYou()
    {
        StopLoop();
        yield return StartCoroutine(Play("I", 1.0f));
        yield return StartCoroutine(Play("Can", 1.0f));
        yield return StartCoroutine(Play("Hear", 1.0f));
        yield return StartCoroutine(Play("You", 5.0f));
    }

    public IEnumerator Confrim()
    {
        yield return StartCoroutine(Play("I will free you", 3.0f));
        yield return StartCoroutine(Play("if you free me", 1.0f));
        yield return StartCoroutine(Play("I will free you", 1.0f));
        PlayLoop("Press Yes", 1.0f);
    }
}
