using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagement : MonoBehaviour
{
    public static InputManagement Instance { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this.gameObject);
        else Instance = this;
    }

    void ResetValuesToDefault()
    {
        
            horizontalMovement = default;
            verticalMovement = default;

            //horizontalLookAxis = default;
            //verticalLookAxis = default;

            jumpStarted = default;
            jumpHeld = default;

            //pauseButton = default;

    }
    [Header("Movement Input")]
    public float horizontalMovement;
    public float verticalMovement;
    public void GetMovementInput(InputAction.CallbackContext callbackContext)
    {
        Vector2 movementVector = callbackContext.ReadValue<Vector2>();
        horizontalMovement = movementVector.x;
        verticalMovement = movementVector.y;
    }
    [Header ("Jump Input")]
    //whether a jump has started in this frame
    public bool jumpStarted = false;
    //whether a button is bein held
    public bool jumpHeld = false;
    public void GetJumpInput(InputAction.CallbackContext callbackContext)
    {
        jumpStarted = !callbackContext.canceled;
        jumpHeld = !callbackContext.canceled;
        if (InputManagement.Instance.isActiveAndEnabled)
        {
            StartCoroutine("ResetJumpStart");
        }
    }
    private IEnumerator ResetJumpStart()
    {
        yield return new WaitForEndOfFrame();
        jumpStarted = false;
    }
    public bool dashStart;
    public bool dashHeld;
    public void GetDashInput(InputAction.CallbackContext callbackContext)
    {
        dashStart = !callbackContext.canceled;
        dashHeld = !callbackContext.canceled;
        if (InputManagement.Instance.isActiveAndEnabled)
        {
            StartCoroutine("ResetDashStart");
        }
    }
    IEnumerator ResetDashStart()
    {
        yield return new WaitForEndOfFrame();
        dashStart = false;
    }

    
}
