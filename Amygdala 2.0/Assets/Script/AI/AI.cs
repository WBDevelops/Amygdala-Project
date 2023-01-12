using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

using UnityEngine.SceneManagement;
public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    public GameObject Shadow;
    [SerializeField] PlayerMovement movement;
    [SerializeField] AudioSource defAmbience;
    public LookWithMouse looking;


    int incrementator;
    public float radius;
    [Range(0, 360)]
    public float angle;
    float distance;

    public GameObject playerRef;
    public GameObject footstep;
    public GameObject ambience;
    public GameObject runsound;
    public GameObject detectionsound;
    public GameObject camera;
    public GameObject deathAngle;
    public GameObject killcam;
    public GameObject neckcrack;
    public GameObject shadowBreathing;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public AudioMixerGroup Dying;

    public bool canSeePlayer;
    bool first = true;
    bool chasing = false;
    bool ready = true;
    bool ready2 = true;
    bool detected = false;
    bool caught = false;
    bool dead = false;

    void Start()
    {
        neckcrack = GameObject.Find("Neck crack");
        killcam.SetActive(false);
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        footstep = GameObject.FindGameObjectWithTag("ShadowFootstep");
        ambience = GameObject.Find("ShadowAmbience");
        runsound = GameObject.Find("ShadowRun");
        detectionsound = GameObject.Find("ShadowDetection");
        camera = GameObject.Find("Main Camera");
        deathAngle = GameObject.Find("Death Angle");
        shadowBreathing = GameObject.Find("ShadowBreathing");

        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Shadow.transform.position, playerRef.transform.position);
        if (Vector3.Distance(Shadow.transform.position, target) < 1.5 && !chasing && !caught)
        {
            Debug.Log("I arrived");
            IterateWaypointIndex();
            UpdateDestination();
            detected = false;

        }
        if (canSeePlayer && !Shadow.GetComponent<Animation>().isPlaying && !chasing && !caught)
        {
            Shadow.GetComponent<Animation>().Play("BeginRun");

            target = playerRef.transform.position;
            agent.SetDestination(target);
            agent.speed = 14f;
            chasing = true;
        }


        if (!Shadow.GetComponent<Animation>().isPlaying && first && !chasing && !caught)
        {
            Shadow.GetComponent<Animation>().Play("BeginWalk4");
            first = false;


        }
        if (!Shadow.GetComponent<Animation>().isPlaying && !chasing && !first && !chasing && !caught)
        {

            Shadow.GetComponent<Animation>().Play("ContinueWalk4");
        }

        if (chasing && !Shadow.GetComponent<Animation>().isPlaying && !caught)
        {
            target = playerRef.transform.position;
            agent.SetDestination(target);
            Shadow.GetComponent<Animation>().Play("Run2");
        }

        if (!chasing && !footstep.GetComponent<AudioSource>().isPlaying && ready && !caught)
        {
            ready = false;
            footstep.GetComponent<AudioSource>().Play();
            StartCoroutine(Delay());
        }

        if (chasing && ready2 && !caught)
        {
            ready2 = false;
            runsound.GetComponent<AudioSource>().Play();
            StartCoroutine(Delay2());
        }


        ambience.GetComponent<AudioSource>().volume = (0.3f / distance);
        if (ambience.GetComponent<AudioSource>().volume < 0.02 || caught)
        {
            defAmbience.volume = 0.096f;
            ambience.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            defAmbience.volume = 0.11f;
        }

        if (movement.isSprinting && !chasing && !caught)
        {

            target = playerRef.transform.position;
            agent.SetDestination(target);
            if (!detected)
            {
                detectionsound.GetComponent<AudioSource>().Play();
            }
            detected = true;

        }

        if (caught && !deathAngle.GetComponent<AudioSource>().isPlaying && !dead)
        {
            dead = true;
            killPlayer();

        }

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Capsule")
        {

            agent.speed = 0;
            Shadow.GetComponent<Animation>().Stop();
            caught = true;
            movement.canMove = false;
            looking.canLook = false;
            killcam.SetActive(true);
            deathAngle.GetComponent<AudioSource>().Play();
            //camera.transform.localPosition = new Vector3(deathAngle.transform.eulerAngles.x,
            // deathAngle.transform.eulerAngles.y, deathAngle.transform.eulerAngles.z);
            Shadow.GetComponent<Animation>().Play("Kill");

        }

    }

    void killPlayer()
    {
        killcam.GetComponent<Rigidbody>().useGravity = true;
        neckcrack.GetComponent<AudioSource>().Play();
        killcam.GetComponent<Rigidbody>().AddForce(0f, 0, 0.000005f);
        shadowBreathing.GetComponent<AudioSource>().outputAudioMixerGroup = Dying;
        incrementator = incrementator + 85;
        Dying.audioMixer.SetFloat("Muffle", 25000 - (incrementator));
        StartCoroutine(LoadDelay());
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }


    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.75f);
        ready = true;
    }

    IEnumerator Delay2()
    {
        yield return new WaitForSeconds(.25f);
        ready2 = true;
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(2);
    }










}
