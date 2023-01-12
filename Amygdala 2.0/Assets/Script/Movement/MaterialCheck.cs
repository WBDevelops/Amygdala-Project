using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCheck : MonoBehaviour
{
    [Header("GroundTypes")]
    Renderer groundType;
    Terrain terrainType;
    [SerializeField] string[] groundMaterial = new string[1];
    [SerializeField] TerrainLayer[] layerMaterial = new TerrainLayer[2];


    [Header("Movement Audio")]
    [SerializeField] AudioClip[] audioClips = new AudioClip[2];
    [SerializeField] GameObject audioGO;
    AudioSource playAudio;

    [SerializeField] CharacterController controller;

    [Header("Scripts")]
    [SerializeField] PlayerMovement movement;

    bool walked;
    bool ran;

    // Start is called before the first frame update
    void Start()
    {
        playAudio = audioGO.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wood"))
            {
                groundType = hit.transform.gameObject.GetComponent<Renderer>();
                WoodMaterial();
            }
            else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Concrete"))
            {
                groundType = hit.transform.gameObject.GetComponent<Renderer>();
                ConcreteMaterial();
            }


            else
            {
                return;
            }


            Debugging(hit);

        }

    }

    private void Debugging(RaycastHit hit)
    {
        Debug.Log(groundType.material);
        //Debug.Log(terrainType.terrainData.terrainLayers[1]);

        Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.yellow);
    }

    private void WoodMaterial()
    {

        if (groundType.material.ToString() == groundMaterial[0])
        {
            if (!movement.horizontalVelocity.Equals(Vector3.zero) && movement.playerCamera.transform.localPosition.y < 0.96 && movement.canMove)
            {
                if (movement.isSprinting)
                {
                    if (walked)
                    {
                        playAudio.Stop();

                    }
                    if (!playAudio.isPlaying)
                    {
                        playAudio.clip = audioClips[1];
                        playAudio.pitch = 0.7f;
                        playAudio.Play();
                        walked = false;
                        ran = true;
                    }
                }
                else
                {
                    if (ran)
                    {
                        playAudio.Stop();
                    }
                    if (!playAudio.isPlaying)
                    {
                        playAudio.clip = audioClips[0];
                        playAudio.pitch = Random.Range(0.82f, 0.88f);
                        playAudio.Play();
                        walked = true;
                        ran = false;
                    }

                }

            }



        }
    }


    private void GrassMaterial()
    {
        if (!movement.horizontalVelocity.Equals(Vector3.zero) && movement.playerCamera.transform.localPosition.y < 0.96 && movement.canMove)
        {
            if (movement.isSprinting)
            {
                if (walked)
                {
                    playAudio.Stop();

                }
                if (!playAudio.isPlaying)
                {
                    int random = Random.Range(2, 7);
                    playAudio.clip = audioClips[random];
                    playAudio.pitch = 0.7f;
                    playAudio.Play();
                    walked = false;
                    ran = true;
                }
            }
            else
            {
                if (ran)
                {
                    playAudio.Stop();
                }
                if (!playAudio.isPlaying)
                {
                    int random = Random.Range(2, 7);
                    playAudio.clip = audioClips[random];
                    playAudio.pitch = Random.Range(0.82f, 0.88f);
                    playAudio.Play();
                    walked = true;
                    ran = false;
                }

            }

        }
    }



    void ConcreteMaterial()
    {
        if (groundType.material.ToString() == groundMaterial[2] || groundType.material.ToString() == groundMaterial[3] || groundType.material.ToString() == groundMaterial[4])
        {
            if (!movement.horizontalVelocity.Equals(Vector3.zero) && movement.playerCamera.transform.localPosition.y < 0.96 && movement.canMove)
            {
                if (movement.isSprinting)
                {
                    if (walked)
                    {
                        playAudio.Stop();

                    }
                    if (!playAudio.isPlaying)
                    {
                        playAudio.clip = audioClips[2];
                        playAudio.pitch = 1f;
                        playAudio.Play();
                        walked = false;
                        ran = true;
                    }
                }
                else
                {
                    if (ran)
                    {
                        playAudio.Stop();
                    }
                    if (!playAudio.isPlaying)
                    {
                        playAudio.clip = audioClips[2];
                        playAudio.pitch = Random.Range(0.82f, 0.88f);
                        playAudio.Play();
                        walked = true;
                        ran = false;
                    }

                }
            }
        }

    }
}

