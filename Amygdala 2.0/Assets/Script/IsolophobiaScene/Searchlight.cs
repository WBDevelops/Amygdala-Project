using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchlight : MonoBehaviour
{
    public float Speed = 1.0f;
    public float x = 1.0f;
    public float z = 1.0f;
    public Collider Search_Collider;
    public GameObject DeathScene;

    private Bounds[] walls;
    private Bounds player;

    void Start()
    {
        BoxCollider[] bxcs = GameObject.Find("Player Restricter 3").GetComponents<BoxCollider>();
        walls = new Bounds[bxcs.Length];
        for (int i = 0; i < bxcs.Length; i++)
        {
            walls[i] = bxcs[i].bounds;
        }
        player = GameObject.Find("Capsule").GetComponent<CapsuleCollider>().bounds;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
        for(int i = 0; i < walls.Length; i++)
        {
            if (walls[i].Contains(transform.position))
            {
                float rotation = Random.Range(100,180);
                transform.Rotate(0,rotation,0);
            }
        }
        if (Search_Collider.bounds.Contains(GameObject.Find("Player (1)").transform.position))
        {
            GameObject.Find("Events Opperator").GetComponent<Events_Manager_Aloness>().Event_Number = -1;
        }
    }
}
