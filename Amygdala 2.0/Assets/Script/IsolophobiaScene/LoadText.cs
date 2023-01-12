using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadText : MonoBehaviour
{
    private TextMeshProUGUI LodTxt;
    public float Percent;
    // Start is called before the first frame update
    void Start()
    {
        Percent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Percent >= 0 && Percent <= 100)
        {
            LodTxt = GameObject.Find("LoadText").GetComponent<TextMeshProUGUI>();
            LodTxt.text = Percent.ToString("F1") + "%";
            Slider slider = GameObject.Find("StartLoadSlider").GetComponent<Slider>();
            slider.value = Percent;
            if(Percent < 30.0f && Percent > 20.0f)
            {
                Percent += Time.deltaTime * 3.0f;
            }
            else if (Percent < 70.0f && Percent > 50.0f)
            {
                Percent += Time.deltaTime * 2.5f;
            }
            else if (Percent < 97.0f && Percent > 100.0f)
            {
                Percent += Time.deltaTime * 0.5f;
            }
            else
            {
                Percent += Time.deltaTime * 100.0f;
            }
        } 
        if(Percent >= 100)
        {
            GameObject.Find("Monitor").GetComponent<MonitorDisplay>().OpenDeskTop();
        }
    }
}
