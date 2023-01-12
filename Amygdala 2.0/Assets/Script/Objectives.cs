using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Objectives : MonoBehaviour
{
    [Header("Objectives")]
    [SerializeField] string[] objective = new string[1];
    [SerializeField] public int objectiveNum;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Objective: " + objective[objectiveNum];
    }
}
