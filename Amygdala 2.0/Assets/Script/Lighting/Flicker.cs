using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (!isFlickering)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.02f, 0.03f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(4f, 5f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;

    }
}
