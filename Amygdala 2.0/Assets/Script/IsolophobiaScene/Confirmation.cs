using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Confirmation : MonoBehaviour
{
    private TextMeshProUGUI ConfirmationTxt;
    private TextMeshProUGUI NoTxt;
    private GameObject ConfirmQuestionBox;
    private Transform Cursor;
    private Bounds ButtonOne;
    private Bounds ButtonTwo;
    private bool ButtonTwoNo;
    // Start is called before the first frame update
    void Start()
    {
        Cursor = GameObject.Find("Cursor").GetComponent<Transform>();
        ButtonOne = GameObject.Find("Button One").GetComponent<Collider>().bounds;
        ButtonTwo = GameObject.Find("Button Two").GetComponent<Collider>().bounds;
        ConfirmationTxt = GameObject.Find("WillYouConfirm").GetComponent<TextMeshProUGUI>();
        NoTxt = GameObject.Find("Text Two").GetComponent<TextMeshProUGUI>();
        ConfirmQuestionBox = GameObject.Find("ConfirmQuestionBox");
        ButtonTwoNo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ConfirmQuestionBox.active)
        {
            if (ButtonOne.Contains(Cursor.position))
            {
                //Change Screen To Video And begin ending
                GameObject.Find("Monitor").SetActive(false);
                GameObject.Find("Main Camera").transform.LookAt(GameObject.Find("BeingScreen").transform);
                GameObject.Find("Being").GetComponent<BeingMovement>().Target = 6;
            }
            if (ButtonTwo.Contains(Cursor.position))
            {
                if (ButtonTwoNo)
                {
                    //Change Text and flash Red
                    NoTxt.text = "YES";
                    NoTxt.color = Color.green;
                    StartCoroutine(StartScreenFlashing());
                    GameObject.Find("ConfirmScreenBackground").GetComponent<Image>().color = Color.red;
                    ButtonTwoNo = false;
                }
                else
                {
                    //Change Screen To Video And begin ending
                    GameObject.Find("Monitor").SetActive(false);
                    GameObject.Find("Main Camera").transform.LookAt(GameObject.Find("Screen").transform);
                    GameObject.Find("Being").GetComponent<BeingMovement>().Target = 6;
                }
            }
        }
    }
    public IEnumerator StartScreenFlashing()
    {
        for (int count = 0; count < 4; count++)
        {
            yield return StartCoroutine(ScreenFlash());
            GameObject.Find("ConfirmScreenBackground").GetComponent<Image>().color = Color.white;
        }
    }
    public IEnumerator ScreenFlash()
    {
        GameObject.Find("ConfirmScreenBackground").GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
    }
}
