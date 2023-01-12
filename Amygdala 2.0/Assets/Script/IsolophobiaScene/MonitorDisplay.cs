using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorDisplay : MonoBehaviour
{
    private GameObject SliderGO;
    private GameObject LoadText;
    private GameObject DeskTop;
    private GameObject ConfirmScreen;
    private GameObject WorkWall;
    private GameObject Cursor;

    private GameObject WallSystem;
    private GameObject Files;
    private GameObject Walls;
    private GameObject FileOpenLocation;

    void Start()
    {
        SliderGO = GameObject.Find("StartLoadSlider");
        ConfirmScreen = GameObject.Find("ConfirmScreen");
        LoadText = GameObject.Find("LoadText");
        DeskTop = GameObject.Find("DeskTop");
        WorkWall = GameObject.Find("WorkWall");
        Cursor = GameObject.Find("Cursor");
        WallSystem = GameObject.Find("WallsSystem");
        Files = GameObject.Find("Files");
        Walls = GameObject.Find("Walls");
        FileOpenLocation = GameObject.Find("File Open Location");
        DeskTop.SetActive(false);
        SliderGO.SetActive(false);
        LoadText.SetActive(false);
        WorkWall.SetActive(false);
        ConfirmScreen.SetActive(false);
        Cursor.SetActive(false);
    }

    void Update()
    {
        if (DeskTop.active && this.gameObject.GetComponent<Monitor>().GetFocusing())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject.Find("Mouse").GetComponent<AudioSource>().Play();
            }
        }
    }

    public void StartPC()
    {
        Cursor.SetActive(true);
        SliderGO.SetActive(true);
        LoadText.SetActive(true);
        LoadText.GetComponent<LoadText>().Percent = 0.0f;
    }
    public void ShutDownPC()
    {
        Cursor.SetActive(false);
        SliderGO.SetActive(false);
        LoadText.SetActive(false);
        DeskTop.SetActive(false);
    }
    public void OpenDeskTop()
    {
        Cursor.SetActive(true);
        DeskTop.SetActive(true);
        SliderGO.SetActive(false);
        LoadText.SetActive(false);
    }
    public void OpenFile(string FileName)
    {
        if (FileName == "Work.exe")
        {
            WorkWall.SetActive(true);
        }
        else if(FileName == "NoteMaker.exe")
        {
            Debug.Log("Opening NoteMaker.exe");
        }
    }
    public void CloseFile(string FileName)
    {
        if (FileName == "Work.exe")
        {
            WorkWall.SetActive(false);
        }
        else if (FileName == "NoteMaker.exe")
        {
            Debug.Log("Opening NoteMaker.exe");
        }
    }
    public void OpenConfirmScreen()
    {
        Cursor.SetActive(true);
        ConfirmScreen.SetActive(true);
    }
}
