using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactions : MonoBehaviour
{

    [Header("Ignore Layer")]
    [SerializeField] LayerMask ignoreMe;

    [Header("Interactable Objects")]
    [SerializeField] GameObject radioNoise;
    [SerializeField] GameObject reader;

    bool isClosed;
    bool reading = false;

    [Header("Animations")]
    [SerializeField] Animator fade;
    [SerializeField] Animator crosshairAnim;
    [SerializeField] GameObject crosshairImage;

    [Header("Raycast")]
    [SerializeField] float raycastLength = 3.5f;
    [SerializeField] string groundType;

    [Header("Scripts")]
    [SerializeField] PlayerMovement movement;
    [SerializeField] LookWithMouse look;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] HouseEvents houseEvents;


    [SerializeField] public bool disableCrosshair;
    // Start is called before the first frame update
    void Start()
    {
        crosshairAnim = GameObject.Find("Crosshair").GetComponent<Animator>();
        crosshairImage = GameObject.Find("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastLength, ~ignoreMe))
        {
            Mirror(hit);

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Interaction"))
            {
                crosshairAnim.SetBool("isInteracting",true);
              

            }
            else
            {
                crosshairAnim.SetBool("isInteracting", false);
            }
          
        }
        else
        {
            crosshairAnim.SetBool("isInteracting", false);
        }


        if(disableCrosshair)
        {
            crosshairImage.SetActive(false);
        }
        else
        {
            crosshairImage.SetActive(true);
        }


    }



    public void Raycast()
    {
       
        if (reading)
            {
                canvasGroup.alpha = 0;
                movement.canMove = true;
               
                reading = false;
            }

       


        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastLength, ~ignoreMe)) 
        {
            Debugging(hit);

            Bed(hit);

            if (!houseEvents.cantTurnLightsOn)
            {
                LightSwitches(hit);
            }

            Radio(hit);

            if (!houseEvents.cantOpenDoor)
            {
                Door(hit);
            }
            Paper(hit);

           


        }

    }

    void Debugging(RaycastHit hit)
    {
        Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
        Debug.Log(hit.transform.name);
    }

    private void Paper(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Paper") && !reading)
        {

            switch (reading)
            {
                case true:
                    Debug.Log("wanna have sex?");
                    canvasGroup.alpha = 0;
                    reading = false;
                    movement.canMove = true;
                    break;
                case false:
                    Debug.Log("I wanna suck your cock");
                    hit.transform.gameObject.GetComponent<AudioSource>().Play();
                    movement.canMove = false;
                    canvasGroup.alpha = 1;
                    reader.GetComponent<Image>().sprite = hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite;
                    reading = true;
                    break;
            }
           
        }
        
    }

    private void Door(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Door"))
        {
            if (hit.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DoorClosed") || hit.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CloseDoor"))
            {

                hit.transform.parent.GetComponent<Animator>().SetTrigger("Open");
                hit.transform.GetComponent<MeshCollider>().enabled = false;
                StartCoroutine(DisableCollider(hit));

            }

            if (hit.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("OpenDoor"))
            {
                hit.transform.parent.GetComponent<Animator>().SetTrigger("Close");
                hit.transform.GetComponent<MeshCollider>().enabled = false;
                StartCoroutine(DisableCollider(hit));
            }
        }
    }
    

    IEnumerator DisableCollider(RaycastHit hit)
    {
        yield return new WaitForSeconds(1);

        hit.transform.GetComponent<MeshCollider>().enabled = true;
    }

    private void LightSwitches(RaycastHit hit)
    {
        if (hit.transform.CompareTag("LightSwitch"))
        {
            Debug.Log("Hit lightswitch");
            GameObject.Find("Click Noise").GetComponent<AudioSource>().Play();
            if (hit.transform.gameObject.GetComponent<LightSwitches>().lights.activeSelf == true)
            {
                hit.transform.gameObject.GetComponent<LightSwitches>().lights.SetActive(false);
            }
            else
            {
                hit.transform.gameObject.GetComponent<LightSwitches>().lights.SetActive(true);
            }

        }
    }

    private void Bed(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Bed"))
        {
            houseEvents.GoToBed();
        }
    }

    private void Radio(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Radio") && radioNoise.activeSelf == true)
        {
            houseEvents.StopRadio();

            Debug.Log("turn off radio");
        }
    }

    private void Mirror(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Mirror"))
        {
            houseEvents.MirrorSpawn();

          
        }
    }

}
