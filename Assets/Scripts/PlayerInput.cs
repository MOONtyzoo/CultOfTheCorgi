using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, PlayerControls.IPlayerActions
{
    public event UnityAction<Vector2> MovementEvent = delegate { };
    public event UnityAction RollEvent = delegate { };

    private PlayerControls PlayerControls;

    private void OnEnable()
    {
        if (PlayerControls == null)
        {
            PlayerControls = new PlayerControls();
            PlayerControls.Player.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void DisableAllInput()
    {
        PlayerControls.Player.Disable();
    }

    public void EnableGameplayInput()
    {
        if (PlayerControls.Player.enabled) return;

        PlayerControls.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RollEvent?.Invoke();
        }
    }
}
