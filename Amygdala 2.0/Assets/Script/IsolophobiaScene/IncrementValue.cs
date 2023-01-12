using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IncrementValue : MonoBehaviour
{
    public int value;
    private bool Hoverd;
    private LineRenderer PlayerLine;
    private TextMeshProUGUI LodTxt;

    void Start()
    {
        LodTxt = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        PlayerLine = GameObject.Find("PlayerLine").GetComponent<LineRenderer>();
        Hoverd = false;
    }

    void Update()
    {
        if (Hoverd)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if(this.gameObject.transform.GetChild(0).GetComponent<Collider>().bounds.Contains(GameObject.Find("Cursor").transform.position) && value > -5)
                {
                    value--;
                }
                if(this.gameObject.transform.GetChild(1).GetComponent<Collider>().bounds.Contains(GameObject.Find("Cursor").transform.position) && value < 5)
                {
                    value++;
                }
                LodTxt.text = value.ToString();
                int Index = (int)char.GetNumericValue(this.gameObject.name[18]);
                Vector3 Position = new Vector3(4 * (Index +1), value, 0);
                PlayerLine.SetPosition(Index +1, Position);
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
