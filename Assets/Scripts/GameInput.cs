using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnbindingRebind;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlt,
        Pause,
    }

    Vector2 inputVector;
    PlayerInput playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInput();

        if (PlayerPrefs.HasKey(PlayerPrefsString.INPUT_BINDINGS_1))
        {
            playerInputActions.LoadBindingOverridesFromJson(
                PlayerPrefs.GetString(PlayerPrefsString.INPUT_BINDINGS_1));
        }

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interaction.performed += Interaction_performed;

        playerInputActions.Player.InterractAlternate.performed += InterractAlternate_performed;

        playerInputActions.Player.Pause.performed += Pause_performed;

    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interaction.performed -= Interaction_performed;

        playerInputActions.Player.InterractAlternate.performed -= InterractAlternate_performed;

        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InterractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

    }

    private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("press E");
        OnInteractAction?.Invoke(this, EventArgs.Empty);

    }

    public Vector2 GetMovementVectorNormalized()
    {
        inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();

            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();

            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();

            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();

            case Binding.Interact:
                return playerInputActions.Player.Interaction.bindings[0].ToDisplayString();

            case Binding.InteractAlt:
                return playerInputActions.Player.InterractAlternate.bindings[0].ToDisplayString();

            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();


            default:
                return "GetBindingTex_DEFAULT";
        }
    }
    
    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction = null;
        int bindingIndex = 0;

        switch (binding)
        {
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;

            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;

            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;

            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;

            case Binding.Interact:
                inputAction = playerInputActions.Player.Interaction;
                bindingIndex = 0;
                break;

            case Binding.InteractAlt:
                inputAction = playerInputActions.Player.InterractAlternate;
                bindingIndex = 0;
                break;

            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;

            default:
                break;
        }


        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();

                playerInputActions.Player.Enable();

                onActionRebound();

                PlayerPrefs.SetString(PlayerPrefsString.INPUT_BINDINGS_1, 
                    playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnbindingRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }

}
