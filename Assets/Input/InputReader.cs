using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName ="InputReader", menuName ="Input/InputReader")]
public class InputReader : ScriptableObject, GameInput.IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public event Action RollEvent;
    public event Action PauseEvent;

    private GameInput GameInput;

    private void OnEnable()
    {
        if (GameInput == null)
        {
            GameInput = new GameInput();

            GameInput.Player.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void DisableAllInput()
    {
        GameInput.Player.Disable();
    }

    public void EnableGameplayInput()
    {
        if (GameInput.Player.enabled) return;

        GameInput.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            RollEvent?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) 
            PauseEvent?.Invoke();
    }
}
