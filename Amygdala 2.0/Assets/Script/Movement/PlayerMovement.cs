using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{

    [Header("Character Controller")]
    [SerializeField] CharacterController controller;

    [Header("Movement Speed")]
    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float crouchSpeed = 1.5f;
    [SerializeField] float runSpeed = 10.0f;

    
   

    [Header("Jumping")]
    [SerializeField] float jumpHeight = 3.5f;
    [SerializeField] float gravity = 30.0f;
    bool jump;
    Vector3 verticalVelocity = Vector3.zero;

    [Header("Crouching")]
    [SerializeField] float crouchHeight = 0.5f;
    [SerializeField] float standingHeight = 2.0f;
    [SerializeField] float timeToCrouch = 0.25f;
    [SerializeField] Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Walk Headbob")]
    [SerializeField] float walkBobSpeed = 14.0f;
    [SerializeField] float walkBobAmount = 0.05f;

    [Header("Run Headbob")]
    [SerializeField] float sprintBobSpeed = 18.0f;
    [SerializeField] float sprintBobAmount = 0.1f;

    [Header("Crouch Headbob")]
    [SerializeField] float crouchBobSpeed = 10.0f;
    [SerializeField] float crouchBobAmount = 0.025f;
    
    [Header("Misc Headbob")]
    [SerializeField] public bool wait = false;
    float defaultYPos = 0;
    float timer;

    [Header("Camera")]
    public Camera playerCamera;

    [Header("Disable Controls")]
    [SerializeField] public bool canMove;
    [SerializeField] public bool canJump;
    [SerializeField] public bool canSprint;
    [SerializeField] public bool canCrouch;

    [Header("Misc Variables")]
    public bool isSprinting;
    float speed;
    public Vector3 horizontalVelocity;
    Vector2 horizontalInput;

    private void Awake()
    {
      
        playerCamera = GetComponentInChildren<Camera>();
        defaultYPos = playerCamera.transform.localPosition.y;
        speed = walkSpeed;
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene(3);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (canMove)
        {
            Movement();
        }

        Gravity();

        if (canJump)
        {
            Jumping();
        }

        
        Headbob();
    }

  
    private void Movement()
    {
        horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);
     
    }


    private void Gravity()
    {
        verticalVelocity.y -= gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        if(controller.isGrounded)
        {
            verticalVelocity.y = 0;
        }
    }

    private void Jumping()
    {

        if (controller.isGrounded)
        {
            verticalVelocity.y = 0.0f;

            if (jump)
            {
                verticalVelocity.y = Mathf.Sqrt(2f * jumpHeight * gravity);
                jump = false;

            }
        }

    }


    private void Headbob()
    {
        if (!controller.isGrounded) return;

        if(Mathf.Abs(horizontalVelocity.x) > 0.1f || Mathf.Abs(horizontalVelocity.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : isSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : isSprinting ? sprintBobAmount : walkBobAmount), playerCamera.transform.localPosition.z);
            
            if (playerCamera.velocity.y > 0.1f && !wait){
                wait = true;
               
            }
        else
            {
                
                wait = false;
            }
        }

    }


    public void RecieveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void onJumpPressed()
    {
        if (controller.isGrounded)
        {
            jump = true;
        }
    }

    public void onSprintPressed()
    {
        if (!isCrouching && canSprint)
        {
            isSprinting = true;
            speed = runSpeed;
        }
    }

    public void onSprintRelease()
    {
        if (!isCrouching && canSprint)
        {
            isSprinting = false;
            speed = walkSpeed;
        }
    }

    public void onCrouchPressed()
    {
        if(!duringCrouchAnimation && controller.isGrounded && canCrouch)
        {
           StartCoroutine(CrouchStand());
        }
    }


    private IEnumerator CrouchStand()
    {
        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = controller.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = controller.center;

        speed = isCrouching ? walkSpeed : crouchSpeed;

        

        while (timeElapsed < timeToCrouch)
        {

            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

         controller.height = targetHeight;
         controller.center = targetCenter;
         isCrouching = !isCrouching;
            
        duringCrouchAnimation = false;
    }
}
