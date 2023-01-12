using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour
{
    Transform[] views = new Transform[3];
    public float transitionSpeed;
    public Transform currentView;
    public GameObject backButton;
    public GameObject creditsBackButton;
    public GameObject frame2;
    public GameObject frame1;
    public GameObject frame0;
    private void Start()
    {
        views[0] = (GameObject.Find("MenuView").transform);
        views[1] = (GameObject.Find("CreditsView").transform);
        views[2] = (GameObject.Find("OptionsView").transform);
        backButton = GameObject.Find("Back");
        creditsBackButton = GameObject.Find("CreditsBack");
        frame2 = GameObject.Find("Frame2");
        frame1 = GameObject.Find("Frame1");
        frame0 = GameObject.Find("Frame0");
        backButton.gameObject.SetActive(false);
        creditsBackButton.gameObject.SetActive(false);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
    }

    public void startGame()
    {
        SceneManager.LoadScene("House");
    }

    public void creditsTransition()
    {
        frame0.gameObject.SetActive(false);
        currentView = views[1];
        creditsBackButton.SetActive(true);
    }

    public void optionsTransition()
    {
        frame0.gameObject.SetActive(false);
        frame1.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        currentView = views[2];
    }

    public void leaveOptions()
    {
        frame1.gameObject.SetActive(false);
        frame0.gameObject.SetActive(true);
        currentView = views[0];
        backButton.gameObject.SetActive(false);
    }
    
    public void leaveCredits()
    {
        frame2.gameObject.SetActive(false);
        frame0.gameObject.SetActive(true);
        currentView = views[0];
        creditsBackButton.SetActive(false);
    }
}
