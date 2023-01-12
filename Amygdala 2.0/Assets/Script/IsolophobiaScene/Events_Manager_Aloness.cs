using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events_Manager_Aloness : MonoBehaviour
{
    public float Event_time;
    public float First_event;
    public float First_event_Camera_Trigger_Angle;
    public BoxCollider Collider_1;
    public BoxCollider Collider_2;
    public GameObject RockOne;
    public GameObject RockTwo;
    public GameObject RockOne_Prefab;
    public GameObject Light_Prefab;
    public GameObject KeyBox;
    public GameObject PlayerRestricter;
    public GameObject PlayerRestricter2;
    public GameObject PlayerRestricter3;
    public GameObject Light2_Prefab;
    public GameObject SearchSphere;
    public GameObject Sceneario2;
    public GameObject EndLocal;
    public GameObject StartLocal;
    public GameObject ArmPat;
    public GameObject DeathScreen;
    public GameObject Lamps;

    public int Event_Number;
    private int Event_Number_2;
    private int Counter;
    private GameObject player;
    private bool Kill_Player;
    private GameObject flashScreen;
    private AlonenessAudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        flashScreen = GameObject.Find("flashScreen");
        audioManager = GameObject.Find("Scene Audio Manager").GetComponent<AlonenessAudioManager>();
        Event_Number = 0;
        Counter = 0;
        player = GameObject.Find("Player (1)");
        GameObject.Find("Player (1)").GetComponent<PlayerMovement>().canMove = false;
        GameObject.Find("Main Camera").GetComponent<LookWithMouse>().canLook = false;
        flashScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Event_Number);
        Event_time = Event_time + Time.deltaTime;
        float Camera_Rotation = player.transform.eulerAngles.y;
        if(Event_Number == -1)
        {
            if (GameObject.Find("claw1(Clone)") != null)
            {
                if (Vector3.Distance(GameObject.Find("Player (1)").transform.position, GameObject.Find("claw1(Clone)").transform.position) <= 1.0f)
                {
                    Kill_Player = true;
                    GameObject.Find("Arm Patrol Opperator").GetComponent<Arm_Operator>().kill_Player = true;
                }
                string SphereName = "";
                for (int i = 0; i < 4; i++)
                {
                    SphereName = "Search Sphere (" + i + ")";
                    if (GameObject.Find(SphereName).GetComponent<Collider>().bounds.Contains(GameObject.Find("Player (1)").transform.position))
                    {
                        Kill_Player = true;
                        GameObject.Find("Arm Patrol Opperator").GetComponent<Arm_Operator>().kill_Player = true;
                    }
                }
                if (Kill_Player = true)
                {
                    GameObject.Find("Arm Patrol Opperator").GetComponent<Arm_Operator>().kill_Player = true;
                    GameObject.Find("Player (1)").GetComponent<PlayerMovement>().canMove = false;
                    GameObject.Find("Main Camera").GetComponent<LookWithMouse>().canLook = false;
                    GameObject.Find("Main Camera").transform.LookAt(GameObject.Find("claw1(Clone)").transform);
                }
                if (Vector3.Distance(GameObject.Find("Player (1)").transform.position, GameObject.Find("claw1(Clone)").transform.position) <= 3.0f && Kill_Player == true)
                {
                    DeathScreen.SetActive(true);
                    Destroy(GameObject.Find("claw1(Clone)"));
                    if (Input.anyKey)
                    {
                        Application.LoadLevel(Application.loadedLevel);
                    }
                }
            }
        }
        if(Event_Number == 0)
        {
            if ( GameObject.Find("StartText") != null && (Event_time >= 15 || Input.GetKey("up")))
            {
                GameObject.Find("Player (1)").GetComponent<PlayerMovement>().canMove = false;
                GameObject.Find("Main Camera").GetComponent<LookWithMouse>().canLook = false;
                if (GameObject.Find("StartText").GetComponent<TextMesh>().color.a > 0)
                {
                    GameObject.Find("StartText").GetComponent<TextMesh>().color = new Color(225.0f,225.0f,225.0f, GameObject.Find("StartText").GetComponent<TextMesh>().color.a - Time.deltaTime * 0.5f);
                }
                else
                {
                    if (Input.anyKey)
                    {
                        audioManager.LoopSound("Wind");
                        audioManager.LoopSound("Rain");
                        GameObject.Find("Player (1)").GetComponent<PlayerMovement>().canMove = true;
                        GameObject.Find("Main Camera").GetComponent<LookWithMouse>().canLook = true;
                        GameObject.Find("Start screen").SetActive(false);
                        audioManager.LoopSound("bulb light2", 5.0f, 25.0f);
                        Event_Number_2 = 1;
                    }
                }
            }
            if (Event_Number_2 == 1 && ((Camera_Rotation >= 160) && (Camera_Rotation <= 200)) && (GameObject.Find("InBusStop").GetComponent<Collider>().bounds.Contains(player.transform.position)))
            {
                if(Event_time > 0.5f && Event_time < 1.0f)
                {
                    GameObject.Find("bulb light2").GetComponent<Light>().intensity = 7;
                    GameObject.Find("bulb light").GetComponent<Light>().intensity = 300;
                }
                else if(Event_time > 1.0f && Event_time < 1.5f)
                {
                    GameObject.Find("bulb light2").GetComponent<Light>().intensity = 10;
                    GameObject.Find("bulb light").GetComponent<Light>().intensity = 600;
                }
                else if (Event_time > 1.5f && Event_time < 2.0f)
                {
                    GameObject.Find("bulb light2").GetComponent<Light>().intensity = 7;
                    GameObject.Find("bulb light").GetComponent<Light>().intensity = 300;
                }
                else if (Event_time > 2f && Event_time < 3f)
                {
                    GameObject.Find("bulb light2").GetComponent<Light>().intensity = 10;
                    GameObject.Find("bulb light").GetComponent<Light>().intensity = 600;
                }
                else if (Event_time > 3f && Event_time < 3.5f)
                {
                    GameObject.Find("bulb light2").GetComponent<Light>().intensity = 7;
                    GameObject.Find("bulb light").GetComponent<Light>().intensity = 300;
                }
                else if (Event_time > 3.5f && Event_time < 5f)
                {
                    GameObject.Find("bulb light2").GetComponent<Light>().intensity = 10;
                    GameObject.Find("bulb light").GetComponent<Light>().intensity = 600;
                }
                if (Event_time >= 5)
                    Event_Number_2 = 2;
            }
            else if(Event_Number_2 == 1 && Event_Number == 0)
            {
                Event_time = 0;
            }
            if(Event_Number_2 == 2)
            {
                GameObject.Find("BusClaw").transform.position = new Vector3(GameObject.Find("BusClaw").transform.position.x, GameObject.Find("BusClaw").transform.position.y, (GameObject.Find("BusClaw").transform.position.z + Time.deltaTime * 40));
            }
            if(GameObject.Find("BusClaw") != null && GameObject.Find("InBusStop") != null){
                if (Vector3.Distance(GameObject.Find("InBusStop").transform.position, GameObject.Find("BusClaw").transform.position) <= 10)
                {
                    audioManager.StopSound("Wind");
                    audioManager.StopSound("Rain");
                    Destroy(GameObject.Find("BusClaw"));
                    GameObject.Find("Road").SetActive(false);
                    GameObject.Find("Bus Stop").SetActive(false);
                    GameObject.Find("LampOne").SetActive(false);
                    flashScreen.SetActive(true);
                    Event_time = 0;
                    Event_Number_2 = 3;
                }
            }
            if(Event_Number_2 == 3 && Event_time >= 2.9)
            {
                audioManager.LoopSound("Wind");
                audioManager.LoopSound("Rain");
                flashScreen.SetActive(false);
                RockTwo.SetActive(true);
                Event_Number_2 = 0;
                Event_Number = 1;
            }
        }
        if (Event_Number == 1 && ((Camera_Rotation <= 45) || (Camera_Rotation >= 345)) && (Collider_1.bounds.Contains(player.transform.position)))
        {
            RockOne.SetActive(true);
            RockTwo.SetActive(false);
            GameObject.Find("twees").SetActive(false);
            Lamps.SetActive(true);
            Event_Number = 2;
        }
        if (Event_Number == 2 && ((Camera_Rotation >= 165 && (Camera_Rotation <= 195)) && (Collider_2.bounds.Contains(player.transform.position))))
        {
            Event_Number = 3;
        }
        if (Event_Number == 3 && ((Camera_Rotation <= 90) || (Camera_Rotation >= 270)))
        {
            RockOne.transform.position = new Vector3(player.transform.position.x, RockOne.transform.position.y, player.transform.position.z - 10.0f);
            Event_Number = 4;
        }
        if (Event_Number == 4)
        {
            RaycastHit Hitinfo;
            Ray ray = new Ray(player.transform.position, player.transform.forward);
            bool hit = Physics.Raycast(ray, out Hitinfo, Mathf.Infinity, 7, QueryTriggerInteraction.UseGlobal);
            float distance = Vector3.Distance(player.transform.position, Hitinfo.collider.gameObject.transform.position);
            if (Hitinfo.collider.gameObject.name == "Rock One" && ((Camera_Rotation >= 170) && (Camera_Rotation <= 190)) && distance <= 5)
            {
                string GameObjectName = "Rock Spawn place ";
                Event_time = 0;
                Event_Number = 5;
                for (int i = 1; i < 6; i++)
                {
                    string NextGOName = GameObjectName + "(" + i + ")";
                    GameObject SpawnLocation = GameObject.Find(NextGOName);
                    Instantiate(RockOne_Prefab, SpawnLocation.transform);
                }
                GameObject.Find("North Wall").SetActive(false);
                PlayerRestricter.SetActive(true);
            }
        }
        if (Event_Number == 5 && Event_time >= 2.5f)
        {
            Event_time = 1.5f;
            Event_Number_2++;

            if (Event_Number_2 <= 6)
            {
                string GameObjectName = "Light Spawn place ";
                string NextGOName = GameObjectName + "(" + Event_Number_2 + ")";
                GameObject SpawnLocation = GameObject.Find(NextGOName);
                GameObject Light = Instantiate(Light_Prefab, SpawnLocation.transform);
                LookTowards lookTowards = Light.GetComponent<LookTowards>();
                lookTowards.Target = KeyBox.transform.parent.gameObject;
                lookTowards.Active = true;
            }
            else
            {
                KeyBox.SetActive(true);
                PlayerRestricter.SetActive(false);
                Event_Number = 6;
                Event_Number_2 = 0;
            }
        }
        if (Event_Number == 6)
        {
            RaycastHit Hitinfo;
            Ray ray = new Ray(player.transform.position, player.transform.forward);
            bool hit = Physics.Raycast(ray, out Hitinfo, Mathf.Infinity, 7, QueryTriggerInteraction.UseGlobal);
            float distance = Vector3.Distance(player.transform.position, Hitinfo.collider.gameObject.transform.position);
            if (Hitinfo.collider.gameObject.name == "WallClock" && distance <= 2)
            {
                GameObject.Find("Scenario One").SetActive(false);
                Sceneario2.SetActive(true);
                PlayerRestricter2.SetActive(true);
                PlayerRestricter3.SetActive(true);
                Event_Number = 7;
            }
        }
        if (Event_Number == 7 && Camera_Rotation >= 10 && Camera_Rotation <= 90)
        {
            KeyBox.SetActive(false);
            audioManager.StopSound("Rain");
            audioManager.StopSound("Wind");
            Event_time = 0;
            Event_Number = 8;
            Event_Number_2 = 0;
        }
        if (Event_Number == 8)
        {
            if (Event_Number_2 >= 6)
            {
                Event_Number = 9;
                Event_Number_2 = 0;
                Event_time = 0;
                PlayerRestricter2.SetActive(false);
            }
            else
            {
                GameObject gameObject = GameObject.Find("Location One");
                Transform[] Objects = gameObject.GetComponentsInChildren<Transform>(true);
                GameObject[] Components = new GameObject[6];
                int y = 5;
                for (int i = Objects.Length - 1; i >= 0; i--)
                {
                    switch (Objects[i].gameObject.name)
                    {
                        case "Bamboo_One":
                            Components[y] = Objects[i].gameObject;
                            y--;
                            break;
                        case "Mount_Part_1":
                            Components[y] = Objects[i].gameObject;
                            y--;
                            break;
                        case "Mount_Part_2":
                            Components[y] = Objects[i].gameObject;
                            y--;
                            break;
                        case "Mount_Part_3":
                            Components[y] = Objects[i].gameObject;
                            y--;
                            break;
                        case "Mount_Part_4":
                            Components[y] = Objects[i].gameObject;
                            y--;
                            break;
                        case "Mount_Part_5":
                            Components[y] = Objects[i].gameObject;
                            y--;
                            break;
                    }
                }
                if (Event_Number_2 == 0)
                {
                    if (Event_time >= 5 && Camera_Rotation >= 10 && Camera_Rotation <= 90)
                    {
                        Event_time = 0;
                        Components[Event_Number_2].SetActive(true);
                        Event_Number_2++;
                    }
                }
                if (Event_Number_2 > 0 && Camera_Rotation >= 45 && Camera_Rotation <= 135)
                {
                    Components[Event_Number_2].SetActive(true);
                }
                else if ((Camera_Rotation >= 315 || Camera_Rotation <= 45) && Event_Number_2 > 0)
                {
                    if (Components[Event_Number_2].active)
                    {
                        Event_Number_2++;
                        Components[0].SetActive(false);
                    }
                }
            }
        }
        if (Event_Number == 9)
        {
            if(Event_Number_2 > 5)
            {
                Event_Number++;
                Event_Number_2 = 0;
                string SphereName = "";
            }
            else
            {
                string component = "";
                switch (Event_Number_2)
                {
                    case 0:
                        component = "floor";
                        break;
                    case 1:
                        component = "North";
                        break;
                    case 2:
                        component = "East";
                        break;
                    case 3:
                        component = "West";
                        break;
                    case 4:
                        component = "South";
                        break;
                    case 5:
                        component = "cieling";
                        break;
                }
                GameObject Tiles = GameObject.Find("5*5 " + component);
                Transform[] TileGOs = Tiles.GetComponentsInChildren<Transform>(true);
                if(Counter < TileGOs.Length && Event_time > 0.1f)
                {
                    Event_time = 0.0f;
                    TileGOs[Counter].gameObject.SetActive(true);
                    Counter++;
                }
                else if(Counter >= TileGOs.Length)
                {
                    Event_Number_2++;
                    Counter = 0;
                }
            }
        }
        if (Event_Number == 10)
        {
            string SpawnName = "";
            string SphereName = "";
            for (int i = 0; i < 6; i++)
            {
                SpawnName = "Tip (" + i + ")";
                SphereName = "Search Sphere (" + i + ")";
                GameObject SpawnLocation = GameObject.Find(SpawnName);
                GameObject Light = Instantiate(Light2_Prefab, GameObject.Find(SpawnName).transform);
                LookTowards lookTowards = Light.GetComponent<LookTowards>();
                lookTowards.Target = GameObject.Find(SphereName);
                Searchlight Sphere = GameObject.Find(SphereName).GetComponent<Searchlight>();
                Sphere.Speed = 20;
            }
            Event_Number++;
        }
        if(Event_Number == 11)
        {
            StartLocal.SetActive(true);
            EndLocal.SetActive(true);
            ArmPat.SetActive(true);
        }
    }
}
