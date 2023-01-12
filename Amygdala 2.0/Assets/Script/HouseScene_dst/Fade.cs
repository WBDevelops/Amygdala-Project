using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Fade : MonoBehaviour
{

    [Header("Animations")]
    [SerializeField] public Animator fade;

    [Header("Scripts")]
    [SerializeField] PlayerMovement movement;
    [SerializeField] LookWithMouse look;
   

    public IEnumerator FadeToBlack()
    {
        if (fade.GetBool("isFadeToBlack") == true)
        {
            movement.canMove = false;
            movement.canCrouch = false;
            movement.canSprint = false;
            movement.horizontalVelocity = new Vector3(0, 0, 0);
            look.canLook = false;

            yield return new WaitForSeconds(2.5f);
            fade.SetBool("isFadeToBlack", false);
            movement.canMove = true;
            movement.canCrouch = true;
            movement.canSprint = true;
            look.canLook = true;

       
        }

    }
}
