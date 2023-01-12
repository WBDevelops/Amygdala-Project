using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DailyTasksScreen : MonoBehaviour
{
    private GameObject ScreenTitle;
    private GameObject ScreenBody;
    private GameObject BreakTitle;
    private GameObject BreakBody;
    private TextMeshProUGUI BreakBodyText;
    private bool DoSquats;
    private bool DoRunning;
    private int Squats;
    private float TimeSpentRunning;
    private int BreaksHad;

    private GameObject workWall;
    // Start is called before the first frame update
    void Start()
    {
        workWall = GameObject.Find("WorkWall");
        ScreenTitle = GameObject.Find("ScreenTitle");
        ScreenBody = GameObject.Find("ScreenBody");
        BreakTitle = GameObject.Find("Break title");
        BreakBody = GameObject.Find("BreakBody");
        BreakBodyText = BreakBody.GetComponent<TextMeshProUGUI>();
        BreakTitle.SetActive(false);
        BreakBody.SetActive(false);
        DoSquats = false;
        DoRunning = false;
        Squats = 0;
        TimeSpentRunning = 0.0f;
        BreaksHad = 0;
    }
    void Update()
    {
        if (BreakBody.active)
        {
            if (GameObject.Find("Chair").GetComponent<Chair>().ToggleSit)
            {
                BreakBodyText.text = "Stand";
            }
            if (!GameObject.Find("Chair").GetComponent<Chair>().ToggleSit)
            {
                BreakBodyText.text = "Do Some Squats";
                DoSquats = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl) && DoSquats)
            {
                Squats++;
            }
            if (Squats >= 10)
            {
                BreakBodyText.text = "Run Around";
                DoRunning = true;
            }
            if (Input.GetKey(KeyCode.LeftShift) && DoRunning)
            {
                TimeSpentRunning += Time.deltaTime;
            }
            if(TimeSpentRunning >= 20.0f)
            {
                GameObject.Find("Computer").GetComponent<ComputerToggle>().ForceOFF = false;
                BreaksHad++;
                SwitchScreen();
            }
        }
    }
    public void SwitchScreen()
    {
        if (ScreenTitle.active)
        {
            ScreenTitle.SetActive(false);
            ScreenBody.SetActive(false);
            BreakBody.SetActive(true);
            BreakTitle.SetActive(true);
        }
        else
        {
            ScreenTitle.SetActive(true);
            ScreenBody.SetActive(true);
            BreakBody.SetActive(false);
            BreakTitle.SetActive(false);
            DoSquats = false;
            DoRunning = false;
            Squats = 0;
            TimeSpentRunning = 0.0f;
            if (BreaksHad == 2)
            {
                GameObject.Find("Being").GetComponent<BeingMovement>().ICanHearYou();
            }
            
        }
    }
}
