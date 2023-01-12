using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint_Spawner : MonoBehaviour
{
    public GameObject Joint;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= 0.05f)
        {
            Timer = 0.0f;
            GameObject joint = Instantiate(Joint, transform.position, transform.rotation);
            joint.transform.LookAt(this.gameObject.transform);
        }
    }
}
