using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_Operator : MonoBehaviour
{
    public GameObject Claw_Prefab;
    public bool kill_Player;

    private GameObject Start_Location;
    private GameObject End_Location;
    private GameObject Claw_Current;
    private Queue<GameObject> Joint_Current = new Queue<GameObject>();
    private float Spawn_Timer;
    private GameObject[] Tiles;
    // Start is called before the first frame update
    void Start()
    {
        kill_Player = false;
        Tiles = GameObject.FindGameObjectsWithTag("Wall");
        Debug.Log(Tiles.Length);
        Debug.Log(Tiles[100].transform.position.x);
        Start_Location = GameObject.Find("Start_Location");
        End_Location = GameObject.Find("End_Location");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("claw1(Clone)") == null && GameObject.Find("Joint(Clone)") == null)
        {
            int NumOne = Random.Range(0, Tiles.Length -1);
            int NumTwo = Random.Range(0, Tiles.Length -1);
            Start_Location.transform.position = (Tiles[NumOne]).transform.position;
            End_Location.transform.position = (Tiles[NumTwo]).transform.position;
            Start_Location.transform.rotation = (Tiles[NumOne]).transform.rotation;
            End_Location.transform.rotation = (Tiles[NumTwo]).transform.rotation;
            if (Vector3.Distance(Start_Location.transform.position, End_Location.transform.position) >= 30.0f)
            {
                Claw_Current = Instantiate(Claw_Prefab, Start_Location.transform);
            }
        }
        else if(GameObject.Find("claw1(Clone)") != null)
        {
            if (Vector3.Distance(Claw_Current.transform.position, End_Location.transform.position) <= 1.5f)
            {
                Destroy(Claw_Current);
            }
            else
            {
                if(kill_Player == false)
                {
                    Claw_Current.transform.LookAt(End_Location.transform);
                }
                else
                {
                    Claw_Current.transform.LookAt(GameObject.Find("Main Camera").transform);
                }
                Claw_Current.transform.position += Claw_Current.transform.forward * 0.5f;
                if (Vector3.Distance(GameObject.Find("Player (1)").transform.position, GameObject.Find("claw1(Clone)").transform.position) <= 1.0f)
                {
                    GameObject.Find("Events Opperator").GetComponent<Events_Manager_Aloness>().Event_Number = -1;
                }
            }

        }
    }
}
