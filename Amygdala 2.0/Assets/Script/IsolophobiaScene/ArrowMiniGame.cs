using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMiniGame : MonoBehaviour
{
    private int[] AnswerContainer;
    public GameObject Arrow;
    public GameObject BoltBox;
    public GameObject BoltArrow;

    private Collider CloseButton;
    private Collider MinimiseButton;
    private Collider SubmitButton;

    private GameObject Cursor;
    private Generator GeneratorCode;

    void Start()
    {
        SubmitButton = GameObject.Find("SubmitButton").GetComponent<Collider>();
        CloseButton = GameObject.Find("CloseButton").GetComponent<Collider>();
        MinimiseButton = GameObject.Find("MinimiseButton").GetComponent<Collider>();
        Cursor = GameObject.Find("Cursor");
        GeneratorCode = GameObject.Find("Generator").GetComponent<Generator>();
    }

    void Update()
    {
        if (SubmitButton.bounds.Contains(Cursor.transform.position))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                bool Correct = true;
                for (int Count = 1; Count < 31; Count++)
                {
                    if (!(GameObject.Find("Arrow (" + Count + ")").GetComponentInChildren<ArrowController>().value == AnswerContainer[Count - 1]))
                    {
                        Correct = false;
                    }
                }
                if (Correct)
                {
                    GeneratorCode.StartGenerator();
                    CloseGame();
                    GameObject.Find("WorkWall").GetComponent<WorkWall>().CloseArrowGame(true);
                    Debug.Log("Correct");
                }
                else
                {
                    //DO the Incorrect
                    Debug.Log("Incorrect");
                }
            }
        }
        if (CloseButton.bounds.Contains(Cursor.transform.position))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                CloseGame();
                GameObject.Find("WorkWall").GetComponent<WorkWall>().CloseWorkWall();
            }
        }
    }

    public void StartGame()
    {
        int RN = Random.Range(0, 4);
        switch(RN)
        {
            case 0:
                AnswerContainer = new int[30] { 4, 0, 0, 0, 1, 1, 2, 2, 2, 2, 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 1, 0, 1, 0, 1, 0, 3, 0, 3, 8 };
                break;
            case 1:
                //AnswerContainer = new int[30] { 4,0,0,0,1,4,0,0,1,1,0,0,8,1,1,3,2,2,2,1,0,0,0,8,1,3,2,2,2,2};
                AnswerContainer = new int[30] { 4, 0, 0, 0, 5, 0, 4, 0, 1, 1, 3, 0, 5, 1, 1, 3, 3, 8, 1, 1, 3, 3, 2, 2, 1, 7, 2, 2, 2, 6 };
                break;
            case 2:
                //AnswerContainer = new int[30] { 5,0,0,0,1,1,7,2,1,2,1,4,3,0,1,1,3,5,2,2,1,3,0,0,5,0,7,8,2,2};
                AnswerContainer = new int[30] { 4, 1, 0, 1, 8, 3, 5, 3, 1, 3, 3, 1, 3, 1, 3, 3, 1, 3, 1, 3, 7, 1, 3, 1, 3, 3, 4, 3, 0, 3 };
                break;
            case 3:
                //AnswerContainer = new int[30] { 5,0,5,0,5,0,3,0,3,1,5,2,2,6,2,0,0,0,0,5,5,1,1,1,6,0,4,0,0,8};
                AnswerContainer = new int[30] { 8, 2, 2, 2, 2, 0, 0, 4, 0, 3, 3, 2, 2, 2, 6, 4, 0, 0, 0, 1, 1, 2, 6, 2, 2, 0, 0, 0, 0, 8 };
                break;
        }
        for(int Count = 1; Count < 31; Count++)
        {
            GameObject GOType;
            int Rotation = 0;
            int Value = 0;
            if(AnswerContainer[Count - 1] == 8)
            {
                GOType = BoltBox;
                Rotation = 0;
                Value = 8;
            }
            else if(AnswerContainer[Count - 1] > 3)
            {
                GOType = BoltArrow;
                Rotation = AnswerContainer[Count - 1] - 4;
                Value = AnswerContainer[Count - 1];
            }
            else
            {
                GOType = Arrow;
                Rotation = 0;
                Value = 0;
            }
            Debug.Log(Count);
            GameObject GO = Instantiate(GOType, GameObject.Find("Arrow (" + Count + ")").transform);
            GO.transform.localRotation = Quaternion.Euler(0,0,(-90 * Rotation));
            GO.GetComponent<ArrowController>().value = Value;
        }
    }

    public void CloseGame()
    {
        for (int Count = 1; Count < 31; Count++)
        {
            Destroy(GameObject.Find("Arrow (" + Count + ")").transform.GetChild(0).gameObject);
        }
    }

}
