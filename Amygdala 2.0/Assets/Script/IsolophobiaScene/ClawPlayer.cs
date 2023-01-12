using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawPlayer : MonoBehaviour
{
    public GameObject Claw_Prefab;
    public bool kill_Player;

    private GameObject Start_Location;
    private GameObject End_Location;
    private GameObject Claw_Current;
    private Queue<GameObject> Joint_Current = new Queue<GameObject>();
    private float Spawn_Timer;
    // Start is called before the first frame update
    void Start()
    {
        kill_Player = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("claw1(Clone)") == null && GameObject.Find("Joint(Clone)") == null)
        {
            Start_Location = GameObject.Find("clawAttack");
            End_Location = GameObject.Find("Main Camera");
            if (Vector3.Distance(Start_Location.transform.position, End_Location.transform.position) >= 30.0f)
            {
                Claw_Current = Instantiate(Claw_Prefab, Start_Location.transform);
            }
        }
        else if (GameObject.Find("claw1(Clone)") != null)
        {
            if (Vector3.Distance(Claw_Current.transform.position, End_Location.transform.position) <= 1.5f)
            {
                Destroy(Claw_Current);
            }
            else
            {
                if (kill_Player == false)
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
                    //end
                    Debug.Log("End");
                    Application.LoadLevel(Application.loadedLevel);
                }
            }

        }
    }
}
