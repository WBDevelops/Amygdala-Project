using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScene : MonoBehaviour
{
    private TextMeshPro EndText;
    public float EndingIn;
    // Start is called before the first frame update
    void Start()
    {
        EndText = GameObject.Find("EndText").GetComponent<TextMeshPro>();
        EndingIn = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        EndingIn -= Time.deltaTime;
        if(EndingIn <= 0)
        {
            Application.Quit();
        }
        else if(EndingIn <= 10)
        {
            EndText.fontSize = 22;
            EndText.text = "I can help";
        }
        else if(EndingIn <= 20)
        {
            EndText.fontSize = 22;
            EndText.text = "I am glad";
        }
        else if(EndingIn <= 30)
        {
            EndText.fontSize = 22;
            EndText.text = "too long";
        }
        else if(EndingIn <= 40)
        {
            EndText.fontSize = 22;
            EndText.text = "It has been";
        }
        else if(EndingIn <= 50)
        {
            EndText.fontSize = 22;
            EndText.text = "Good.";
        }
    }
}
