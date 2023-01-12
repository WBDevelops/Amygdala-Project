using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class File : MonoBehaviour
{
    public bool Hoverd;
    public bool Clicked;
    public float Timer;
    void Start()
    {
        Hoverd = false;
        Timer = 0.0f;
    }

    void Update()
    {
        if (Hoverd)
        {
            if (Input.GetButtonDown("Fire1") && !Clicked)
            {
                Clicked = true;
                Timer = 0.0f;
            }
            else if (Input.GetButtonDown("Fire1") && Clicked && Timer < 2.0f)
            {
                GameObject.Find("Monitor").GetComponent<MonitorDisplay>().OpenFile(transform.gameObject.name);
                Clicked = false;
                Timer = 2.01f;
            }
            else if (Timer > 2.0f)
            {
                Clicked = false;
                Timer = 2.01f;
            }
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.name == "Cursor")
        {
            Timer += Time.deltaTime;
            GameObject.Find("Cursor").GetComponent<Image>().sprite = GameObject.Find("HandSprite").GetComponent<Image>().sprite;
            Hoverd = true;
        }
    }
    void  OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Cursor")
        {
            GameObject.Find("Cursor").GetComponent<Image>().sprite = GameObject.Find("MouseSprite").GetComponent<Image>().sprite;
            Hoverd = false;
        }
    }
}
