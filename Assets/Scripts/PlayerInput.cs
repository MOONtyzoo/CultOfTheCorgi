using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, CorgiControls.IPlayerActions
{
    public event UnityAction<Vector2> MovementEvent = delegate { };

    private CorgiControls _playerActions;

    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new CorgiControls();
            _playerActions.Player.SetCallbacks(this);
            // _playerActions.UI.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void DisableAllInput()
    {
        _playerActions.Player.Disable();
    }

    public void EnableGameplayInput()
    {
        if (_playerActions.Player.enabled) return;

        // _playerActions.UI.Disable();
        _playerActions.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
