using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoMiniGame : MonoBehaviour
{
    private Collider CloseButton;
    private Collider MinimiseButton;
    private Collider SubmitButton;
    private GameObject Cursor;

    public bool[] values = new bool[25];
    public int ID;
    private bool Hoverd;
    // Start is called before the first frame update
    void Start()
    {
        CloseButton = GameObject.Find("CloseButton").GetComponent<Collider>();
        MinimiseButton = GameObject.Find("MinimiseButton").GetComponent<Collider>();
        SubmitButton = GameObject.Find("SubmitButton").GetComponent<Collider>();
        Cursor = GameObject.Find("Cursor");
        Hoverd = false;
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (SubmitButton.bounds.Contains(Cursor.transform.position))
            {
                Submit();
            }
            int count = 1;
            bool InSquare = false;
            foreach(GameObject Squares in GameObject.FindGameObjectsWithTag("SquareTile"))
            {
                if (Squares.GetComponent<Collider>().bounds.Contains(Cursor.transform.position))
                {
                    if(Squares.GetComponent<Image>().color == Color.white)
                    {
                        Cursor.GetComponent<Image>().color = Color.white;
                        InSquare = true;
                    }
                    PressSquare(count);
                }
                count++;
            }
            if (!InSquare)
            {
                Cursor.GetComponent<Image>().color = Color.white;
            }
        }
    }
    public void Submit()
    {
        bool Correct = true;
        for (int count = 0; count < 24; count++)
        {
            if (values[count] != true)
            {
                Correct = false;
            }
        }
        if (Correct)
        {
            CloseGame();
            GameObject.Find("WorkWall").GetComponent<WorkWall>().CloseVideoMiniGame(true);
            Debug.Log("Correct");
        }
        else
        {
            ResetGame();
            Debug.Log("Incorrect");
        }
    }
    public void ResetGame()
    {
        for (int count = 0; count < 25; count++)
        {
            Debug.Log("Count :" + count);
            values[count] = true;
            GameObject.Find("Square (" + (count + 1) + ")").GetComponent<Image>().color = Color.white;
        }
        StartGame(25);
    }
    public void StartGame(int Flips)
    {
        for (int count = 1; count < Flips; count++)
        {
            int ID = Random.Range(1, 25);
            PressSquare(ID);
        }
    }

    public void PressSquare(int ID)
    {
        int IDLeft = ID - 1;
        int IDTop = ID + 5;
        int IDBottom = ID - 5;
        int IDRight = ID + 1;
        values[ID - 1] = FlipValue(ID);
        if (IDLeft > 0 && ((IDLeft % 10) != 5) && ((IDLeft % 10) != 0))
        {
            values[IDLeft - 1] = FlipValue(IDLeft);
        }
        if (IDRight < 26 && ((IDRight % 5) != 1)){
            values[IDRight - 1] = FlipValue(IDRight);
        }
        if(IDTop < 26)
        {
            values[IDTop - 1] = FlipValue(IDTop);
        }
        if(IDBottom > 0)
        {
            values[IDBottom - 1] = FlipValue(IDBottom);
        }
        Debug.Log(ID + " " + IDLeft + " " + IDRight + " " + IDTop + " " + IDBottom);
    }

    public bool FlipValue(int ID)
    {
        Image Square = GameObject.Find("Square (" + (ID) + ")").GetComponent<Image>();
        if (Square.color == Color.white)
        {
            Square.color = Color.black;
            return false;
        }
        else
        {
            Square.color = Color.white;
            return true;
        }
    }
    public void CloseGame()
    {

    }
}
