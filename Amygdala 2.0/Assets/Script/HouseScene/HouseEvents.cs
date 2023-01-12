using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HouseEvents : MonoBehaviour
{


    [Header("Objects")]
    [SerializeField] int numberOfLightsOn;
    [SerializeField] AudioSource radioNoise;
    [SerializeField] GameObject radioGlow;
    [SerializeField] AudioSource floorThud;
    [SerializeField] Animator doorAnim;
    [SerializeField] GameObject bathroomGlow;
    [SerializeField] GameObject[] unloadSceneObjects = new GameObject[1];
    [SerializeField] GameObject shadowMan;
    [SerializeField] Transform player;

    [Header("Scripts")]
    [SerializeField] Fade fadeToBlack;
    [SerializeField] TriggerDoorClose triggerDoor;
    [SerializeField] Objectives objectives;

    [Header("Triggers")]
    [SerializeField] bool hasSlept = false;
    [SerializeField] bool radioPlaying = false;
    [SerializeField] bool closeBathroomDoor = false;
    [SerializeField] public bool cantTurnLightsOn = false;
    [SerializeField] public bool cantOpenDoor = false;

    [SerializeField] public AudioSource SCARESOUND;


    // Start is called before the first frame update
    void Awake()
    {
        fadeToBlack = GameObject.Find("GameEvents").GetComponent<Fade>();
        triggerDoor = GameObject.Find("BathroomTrigger").GetComponent<TriggerDoorClose>();
        radioNoise = GameObject.Find("RadioNoise").GetComponent<AudioSource>();
        floorThud = GameObject.Find("FloorThudSound").GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        objectives = GameObject.Find("GameEvents").GetComponent<Objectives>();
       
    }

    private void Update()
    {
        TurnOffLights();
        BathroomDoorClose();


    }

    public void TurnOffLights()
    {
        numberOfLightsOn = GameObject.FindGameObjectsWithTag("Lights").Length;

        if(numberOfLightsOn == 0 && objectives.objectiveNum == 1)
        {
            objectives.objectiveNum++;
        }
    }

    public void GoToBed()
    {
        if (!hasSlept && numberOfLightsOn == 0)
        {
            fadeToBlack.fade.SetBool("isFadeToBlack", true);
            StartCoroutine(fadeToBlack.FadeToBlack());

            //Triggers
            hasSlept = true;
            radioPlaying = true;
            cantTurnLightsOn = true;

            StartRadio();
        }
    }


    private void StartRadio()
    {
        if(radioPlaying)
        {
            radioNoise.PlayDelayed(2.5f);
            radioGlow.SetActive(true);

            if (objectives.objectiveNum == 2)
            {
                objectives.objectiveNum++;
            }
        }

       
    }

    public void StopRadio()
    {
        if (radioPlaying)
        {
            radioNoise.Stop();
            radioPlaying = false;

            BathroomEvents();
        }
    }



    private void BathroomEvents()
    {
        floorThud.PlayDelayed(3f);
        
       //doorAnim.SetTrigger("Open");
        bathroomGlow.SetActive(true);
        closeBathroomDoor = true;
    }

    void BathroomDoorClose()
    {
        if (triggerDoor.closeDoor && closeBathroomDoor)
        {
            doorAnim.SetTrigger("Close");
            cantOpenDoor = true;

            for (int i = 0; i < 4; i++)
            {
                Destroy(unloadSceneObjects[i]);
            }
        }
    }


    public void MirrorSpawn()
    {
        Debug.Log("Looked into mirror");

        shadowMan.SetActive(true);
        SCARESOUND.Play();
        StartCoroutine(Delay());
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
}
