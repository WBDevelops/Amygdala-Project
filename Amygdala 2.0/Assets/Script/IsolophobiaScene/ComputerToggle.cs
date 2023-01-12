using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerToggle : MonoBehaviour
{
    private GameObject ScreenON;
    private GameObject ScreenOFF;
    private GameObject ONLight;
    private GameObject OFFLight;
    private float CoolDown;
    private bool ToggleON;
    public bool ForceOFF;
    private AudioSource AudioData;
    // Start is called before the first frame update
    void Start()
    {
        ScreenON = GameObject.Find("Screen On");
        ScreenOFF = GameObject.Find("Screen Off");
        ONLight = GameObject.Find("ON");
        OFFLight = GameObject.Find("OFF");
        ONLight.SetActive(false);
        ScreenON.SetActive(false);
        CoolDown = 0.0f;
        ToggleON = false;
        ForceOFF = false;
        AudioData = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !ForceOFF)
        {
            RaycastHit hit;
            if(Physics.Raycast((GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition)), out hit)){
                if(hit.transform.gameObject == this.gameObject)
                {
                    if(CoolDown <= 0.0f)
                    {
                        if (ToggleON)
                        {
                            CoolDown = 7.0f;
                            ONLight.SetActive(false);
                            ScreenON.SetActive(false);
                            ScreenOFF.SetActive(true);
                            OFFLight.SetActive(true);
                            AudioData.time = 35.0f;
                            AudioData.loop = false;
                            GameObject.Find("Monitor").GetComponent<MonitorDisplay>().ShutDownPC();
                            ToggleON = false;
                        }
                        else
                        {
                            CoolDown = 7.0f;
                            ONLight.SetActive(true);
                            ScreenON.SetActive(true);
                            ScreenOFF.SetActive(false);
                            OFFLight.SetActive(false);
                            AudioData.volume = 0.006f;
                            AudioData.time = 0;
                            AudioData.loop = true;
                            AudioData.Play();
                            GameObject.Find("Monitor").GetComponent<MonitorDisplay>().StartPC();
                            ToggleON = true;
                        }
                    }
                }
            }
        }
        CoolDown -= Time.deltaTime;
        if (AudioData.isPlaying)
        {
            if (AudioData.time >= 30.0f && AudioData.time < 34.9f )
            {
                AudioData.time = 5.0f;
            }
        }
    }
    public void ShutDown()
    {
        if (ToggleON)
        {
            CoolDown = 7.0f;
            ONLight.SetActive(false);
            ScreenON.SetActive(false);
            ScreenOFF.SetActive(true);
            OFFLight.SetActive(true);
            AudioData.time = 35.0f;
            AudioData.loop = false;
            GameObject.Find("Monitor").GetComponent<MonitorDisplay>().ShutDownPC();
            ToggleON = false;
        }
    }
}
