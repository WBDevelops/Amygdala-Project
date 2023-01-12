using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OutsidePlayArea : MonoBehaviour
{

    Transform startingPoint;
    Transform rightPoint;
    Transform leftPoint;
    Transform startingRotation;
    GameObject player;

    Vector3 playerStartLocation;

    Fade fadeToBlack;

    TextMeshProUGUI fadeText;
    [SerializeField] string[] text;

    // Start is called before the first frame update
    void Awake()
    {
       
        player = GameObject.Find("Player");
        fadeText = GameObject.Find("FadeText").GetComponent<TextMeshProUGUI>();
        rightPoint = GameObject.Find("BeginningPointRight").GetComponent<Transform>();
        leftPoint = GameObject.Find("BeginningPointLeft").GetComponent<Transform>();


      

        //BackToBeginning();

        fadeToBlack = GameObject.Find("GameEvents").GetComponent<Fade>();


    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.x >= this.transform.localPosition.x)
        {
            playerStartLocation = new Vector3(rightPoint.position.x, rightPoint.position.y, rightPoint.position.z);
       
        }
        else
        {
            playerStartLocation = new Vector3(leftPoint.position.x, leftPoint.position.y, leftPoint.position.z);
     
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
           
            fadeToBlack.fade.SetBool("isFadeToBlack", true);
            StartCoroutine(fadeToBlack.FadeToBlack());
            StartCoroutine(Teleport());
            
            Debug.Log("Exitting Play area");

        }



    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.1f);

        fadeText.text = text[0];


        yield return new WaitForSeconds(2.5f);

        BackToBeginning();

        yield return new WaitForSeconds(0.1f);

        fadeText.text = "";
    }

    void BackToBeginning()
    {
        Debug.Log("Back to beginning");

        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = playerStartLocation;

        if (player.transform.position.x >= this.transform.localPosition.x)
        {
            player.transform.rotation = rightPoint.rotation;
        }
        else
        {
            player.transform.rotation = leftPoint.rotation;
        }
 
       
        player.GetComponent<CharacterController>().enabled = true;


    }
}
