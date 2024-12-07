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
    public event Action AttackEvent; 
    public event Action<Vector2> LookEvent;
    
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
        
        if (Gamepad.current != null) {
            LookEvent?.Invoke(context.ReadValue<Vector2>());
        }
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

    public void OnAttack(InputAction.CallbackContext context)
    {
       if (context.phase == InputActionPhase.Performed)
          AttackEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (Gamepad.current == null) {
            Vector2 mouseInput = context.ReadValue<Vector2>();
            Vector2 mouseInputFromCenter = mouseInput - new Vector2(Screen.width/2f, Screen.height/2f);
            LookEvent?.Invoke(mouseInputFromCenter.normalized);
        }
    }
}
