using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderShadowDisappear : MonoBehaviour
{
    bool triggered;
    [SerializeField] GameObject shadow;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            shadow.GetComponent<Animation>().Play("ShadowLooking");
            StartCoroutine(Wait());
            triggered = true;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.7f);
        shadow.SetActive(false);
    }
    private void Start()
    {
        
    }
}
