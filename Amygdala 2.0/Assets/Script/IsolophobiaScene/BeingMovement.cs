using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeingMovement : MonoBehaviour
{
    public bool StartWalking;
    public bool HeadGoingUp;
    public int Target;
    private float value;
    private float wait;
    private bool SCANdown;
    private Transform CameraTransform;
    private AudioSource Steps;

    private GameObject nextCamera; 
    // Start is called before the first frame update
    void Start()
    {
        Steps = GameObject.Find("Beings Feets").GetComponent<AudioSource>();
        HeadGoingUp = true;
        CameraTransform = GameObject.Find("BeingCamera").transform;
        StartWalking = false;
        Target = 1;
        value = 1.0f;
        nextCamera = GameObject.Find("NextCamera");
        nextCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == 5)
        {
            StartWalking = false;
        }
        if(Target == 6)
        {
            StartWalking = true;
        }
        Transform NewTargetPos = GameObject.Find("Path (" + Target + ")").transform;
        float damping = 2.0f;
        Vector3 lookPos = NewTargetPos.position - CameraTransform.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        CameraTransform.transform.rotation = Quaternion.Slerp(CameraTransform.rotation, rotation, Time.deltaTime * damping);
        if (StartWalking && Target != 7)
        {
            wait += Time.deltaTime;
            if (wait >= 1.5f)
            {
                Vector3 TargetPos = GameObject.Find("Path (" + Target + ")").transform.position;
                if (Vector3.Distance(transform.position, TargetPos) <= 3.0f)
                {
                    GameObject.Find("Path (" + Target + ")").GetComponent<AudioSource>().Play();
                    if (Target == 4)
                    {
                        Target = 1;
                    }
                    else if(Target < 4 || Target == 6)
                    {
                        Target++;
                    }
                }
                if (CameraTransform.localPosition.y >= 1.2f && HeadGoingUp)
                {
                    PlayStep();
                    value = -0.2f;
                    HeadGoingUp = false;
                }
                if (CameraTransform.localPosition.y <= 0.0f && !HeadGoingUp)
                {
                    wait = 0.0f;
                    value = 0.2f;
                    HeadGoingUp = true;
                }

                NewTargetPos = GameObject.Find("Path (" + Target + ")").transform;
                transform.position = Vector3.MoveTowards(transform.position, NewTargetPos.position, 0.02f);
                CameraTransform.transform.localPosition = new Vector3(CameraTransform.transform.localPosition.x, CameraTransform.localPosition.y + (0.1f * value), CameraTransform.transform.localPosition.z);  
            }
        }
        else if( Target == 7)
        {
            BeginEnd();
        }
    }
    public void ICanHearYou()
    {
        StartCoroutine(ICanHearYou2());
    }
    public IEnumerator ICanHearYou2()
    {
        yield return new WaitForSeconds(30.0f);
        ICanHearYou3();
    }
    public void ICanHearYou3()
    {
        Target = 5;
        Debug.Log("Target:" + Target);
        StartCoroutine(this.gameObject.GetComponent<BeingAudio>().ICanHearYou());
        GameObject.Find("Banging").GetComponent<AudioSource>().Play();
        
    }
    public void Ending()
    {
        Target = 6;
    }
    public void BeginEnd()
    {
        CameraTransform.gameObject.SetActive(false);
        nextCamera.SetActive(true);
        GameObject.Find("Being").SetActive(false);
    }
    private void PlayStep()
    {
        Steps.Play();
        StartCoroutine(StopStep());
    }
    private IEnumerator StopStep()
    {
        yield return new WaitForSeconds(Steps.clip.length - 0.05f);
        Steps.Stop();
    }
}
