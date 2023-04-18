using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput_T : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;


    PlayerInput playerInputActions;

    Vector2 inputVector;

    private void Awake()
    {
        playerInputActions = new PlayerInput();

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interaction.performed += Interaction_performed;

        playerInputActions.Player.InterractAlternate.performed += InterractAlternate_performed;
    }

    private void InterractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("InterractAlternate_performed");
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

    }

    private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Interaction_performed");
        OnInteractAction?.Invoke(this, EventArgs.Empty);

    }


    public Vector2 GetMovementVectorNormalized()
    {
        inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
