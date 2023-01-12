using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    public int value;
    private bool Hoverd;

    void Start()
    {
        Hoverd = false;
    }

    void Update()
    {
        if (Hoverd)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (value < 4)
                {
                    value++;
                    if (value == 4)
                    {
                        value = 0;
                    }
                    transform.localRotation = Quaternion.Euler(0, 0, -90 * value);
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
}
