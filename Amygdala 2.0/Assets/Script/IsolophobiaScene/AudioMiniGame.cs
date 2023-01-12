using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMiniGame : MonoBehaviour
{
    private Collider CloseButton;
    private Collider MinimiseButton;
    private Collider SubmitButton;

    private GameObject Cursor;

    private int[] TargetLinePos;
    private int[] PlayerLinePos;

    private LineRenderer TargetLine;
    private LineRenderer PlayerLine;



    void Start()
    {
        SubmitButton = GameObject.Find("SubmitButton").GetComponent<Collider>();
        CloseButton = GameObject.Find("CloseButton").GetComponent<Collider>();
        MinimiseButton = GameObject.Find("MinimiseButton").GetComponent<Collider>();
        Cursor = GameObject.Find("Cursor");
        TargetLine = GameObject.Find("TargetLine").GetComponent<LineRenderer>();
        PlayerLine = GameObject.Find("PlayerLine").GetComponent<LineRenderer>();
        TargetLinePos = new int[12];
        PlayerLinePos = new int[12];
    }

    void Update()
    {
        if (SubmitButton.bounds.Contains(Cursor.transform.position))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                bool Correct = true;
                for (int Count = 2; Count < 9; Count++)
                {
                    if ((GameObject.Find("Manipulate Value (" + (Count - 1) + ")").GetComponent<IncrementValue>().value != TargetLinePos[Count]))
                    {
                        Correct = false;
                    }
                }
                if (Correct)
                {
                    CloseGame();
                    GameObject.Find("WorkWall").GetComponent<WorkWall>().CloseAudioGame(true);
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
        for(int count = 2; count < 9; count++)
        {
            int Pos = Random.Range(-5, 6);
            TargetLinePos[count] = Pos;
            Vector3 Position = new Vector3(4 * count, Pos, 0);
            TargetLine.SetPosition(count, Position);
        }
    }
    public void CloseGame()
    {

    }
}
