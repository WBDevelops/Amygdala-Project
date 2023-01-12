using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForBig : MonoBehaviour
{
    public GameObject cube1, cube2, cube3, cube4;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            cube1.SetActive(true);
            cube2.SetActive(true);
            cube3.SetActive(true);
            cube4.SetActive(true);
            StartCoroutine(Wait());
            triggered = true;
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        cube1.SetActive(false);
        cube2.SetActive(false);
        cube3.SetActive(false);
        cube4.SetActive(false);
    }
}
