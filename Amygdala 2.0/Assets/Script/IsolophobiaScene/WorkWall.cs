using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkWall : MonoBehaviour
{
    private GameObject ArrowsMiniG;
    private GameObject Buttons;
    private GameObject AudioMiniG;
    private GameObject VideoMiniG;

    private Collider PowerONButton;
    private Collider AudioONButton;
    private Collider VideoONButton;
    private Collider ConfirmButton;

    private GameObject SubmitButton;

    private GameObject Cursor;

    private GameObject BeingScreen;

    private bool Hoverd;

    public bool PowerON;
    public bool AudioON;
    private bool VideoON;
    private bool Confirm;

    // Start is called before the first frame update
    void Start()
    {
        PowerONButton = GameObject.Find("ArrowsGameButton").GetComponent<Collider>();
        AudioONButton = GameObject.Find("AudioGameButton").GetComponent<Collider>();
        VideoONButton = GameObject.Find("VideoGameButton").GetComponent<Collider>();
        ConfirmButton = GameObject.Find("Confirm").GetComponent<Collider>();
        SubmitButton = GameObject.Find("SubmitButton");
        BeingScreen = GameObject.Find("BeingScreen");
        Cursor = GameObject.Find("Cursor");
        Buttons = GameObject.Find("Buttons");
        ArrowsMiniG = GameObject.Find("Arrows");
        AudioMiniG = GameObject.Find("Audio");
        VideoMiniG = GameObject.Find("Video");
        VideoMiniG.SetActive(false);
        AudioMiniG.SetActive(false);
        ArrowsMiniG.SetActive(false);
        SubmitButton.SetActive(false);
        Hoverd = false;
        PowerON = false;
        AudioON = false;
        VideoON = false;
        Confirm = false;
        BeingScreen.SetActive(false);
    }

    void Update()
    {
        if (Hoverd)
        {
            int Selected = 0;
            if (PowerONButton.bounds.Contains(Cursor.transform.position) && !PowerON)
            {
                GameObject.Find("Cursor").GetComponent<Image>().sprite = GameObject.Find("HandSprite").GetComponent<Image>().sprite;
                Selected = 1;
            }
            else if (AudioONButton.bounds.Contains(Cursor.transform.position) && !AudioON && PowerON)
            {
                GameObject.Find("Cursor").GetComponent<Image>().sprite = GameObject.Find("HandSprite").GetComponent<Image>().sprite;
                Selected = 2;
            }
            else if (VideoONButton.bounds.Contains(Cursor.transform.position) && !VideoON && AudioON && PowerON)
            {
                GameObject.Find("Cursor").GetComponent<Image>().sprite = GameObject.Find("HandSprite").GetComponent<Image>().sprite;
                Selected = 3;
            }
            else if (ConfirmButton.bounds.Contains(Cursor.transform.position))
            {
                GameObject.Find("Cursor").GetComponent<Image>().sprite = GameObject.Find("HandSprite").GetComponent<Image>().sprite;
                Selected = 4;
            }
            else
            {
                GameObject.Find("Cursor").GetComponent<Image>().sprite = GameObject.Find("MouseSprite").GetComponent<Image>().sprite;
            }
            if (Input.GetButtonDown("Fire1") && Selected != 0)
            {
                switch (Selected)
                {
                    case 1:
                        SubmitButton.SetActive(true);
                        ArrowsMiniG.SetActive(true);
                        ArrowsMiniG.GetComponent<ArrowMiniGame>().StartGame();
                        Buttons.SetActive(false);
                        break;
                    case 2:
                        SubmitButton.SetActive(true);
                        AudioMiniG.SetActive(true);
                        AudioMiniG.GetComponent<AudioMiniGame>().StartGame();
                        Buttons.SetActive(false);
                        break;
                    case 3:
                        SubmitButton.SetActive(true);
                        VideoMiniG.SetActive(true);
                        Buttons.SetActive(false);
                        VideoMiniG.GetComponent<VideoMiniGame>().StartGame(25);
                        break;
                    case 4:
                        StartCoroutine(GameObject.Find("Being").GetComponent<BeingAudio>().Confrim());
                        GameObject.Find("Monitor").GetComponent<MonitorDisplay>().OpenConfirmScreen();
                        break;
                }
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Cursor")
        {
            GameObject.Find("Cursor").GetComponent<Image>().sprite = GameObject.Find("HandSprite").GetComponent<Image>().sprite;
            Hoverd = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Cursor")
        {
            GameObject.Find("Cursor").GetComponent<Image>().sprite = GameObject.Find("MouseSprite").GetComponent<Image>().sprite;
            Hoverd = false;
        }
    }

    public void CloseArrowGame(bool Complete)
    {
        if (Complete)
        {
            PowerONButton.GetComponent<Image>().color = Color.green;
            SubmitButton.SetActive(false);
            ArrowsMiniG.SetActive(false);
            Buttons.SetActive(true);
            PowerON = true;
            GameObject.Find("Computer").GetComponent<ComputerToggle>().ForceOFF = true;
            GameObject.Find("Monitor").GetComponent<MonitorDisplay>().ShutDownPC();
            GameObject.Find("Computer").GetComponent<ComputerToggle>().ShutDown();
            GameObject.Find("Screen").GetComponent<DailyTasksScreen>().SwitchScreen();
        }
    }
    public void CloseAudioGame(bool Complete)
    {
        if (Complete)
        {
            AudioONButton.GetComponent<Image>().color = Color.green;
            SubmitButton.SetActive(false);
            AudioMiniG.SetActive(false);
            Buttons.SetActive(true);
            AudioON = true;
            GameObject.Find("Being").GetComponent<BeingAudio>().StartBootingUp();
            GameObject.Find("Computer").GetComponent<ComputerToggle>().ForceOFF = true;
            GameObject.Find("Monitor").GetComponent<MonitorDisplay>().ShutDownPC();
            GameObject.Find("Computer").GetComponent<ComputerToggle>().ShutDown();
            GameObject.Find("Screen").GetComponent<DailyTasksScreen>().SwitchScreen();
        }
    }
    public void CloseVideoMiniGame(bool Complete)
    {
        if (Complete)
        {
            VideoONButton.GetComponent<Image>().color = Color.green;
            SubmitButton.SetActive(false);
            VideoMiniG.SetActive(false);
            Buttons.SetActive(true);
            VideoON = true;
            BeingScreen.SetActive(true);
        }
    }
    public void CloseWorkWall()
    {
        GameObject.Find("Monitor").GetComponent<MonitorDisplay>().CloseFile("Work.exe");
    }
}
