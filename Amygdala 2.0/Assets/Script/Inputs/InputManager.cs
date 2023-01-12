using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [Header("Scripts")]
    [SerializeField] PlayerMovement movement;
    [SerializeField] Interactions interactions;

  

    [Header("Inputs")]
    public PlayerInputActions playerInputActions;
    Vector2 horizontalInput;
    Vector2 mouseInput;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        InputMovement();
        InputJump();
        InputSprint();
        InputCrouch();
        InputInteraction();
        
    }

    private void Update()
    {
        movement.RecieveInput(horizontalInput);
    }


    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void InputMovement()
    {
        
            playerInputActions.Player.Movement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
       
    }

    private void InputJump()
    {
            playerInputActions.Player.Jump.performed += _ => movement.onJumpPressed();
        
    }

    private void InputSprint()
    {
            playerInputActions.Player.SprintStart.performed += _ => movement.onSprintPressed();
            playerInputActions.Player.SprintFinish.performed += _ => movement.onSprintRelease();
        
    }

    private void InputCrouch()
    {
        
            playerInputActions.Player.Crouch.performed += _ => movement.onCrouchPressed();
       
    }

    private void InputInteraction()
    {
        playerInputActions.Player.Interact.performed += _ => interactions.Raycast();
    }




}
