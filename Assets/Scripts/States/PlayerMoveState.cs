using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "States/Player/Move")]
public class PlayerMoveState : State<Player>
{
    [SerializeField, Range(0f, 50f)] private float Speed = 25f;

    private Vector2 PlayerMovement;

    public override void Enter(Player parent)
    {
        base.Enter(parent);
        RunnerObject.SetAnimation("PlayerRun");
    }

    public override void Tick(float deltaTime)
    {
        // we need to normalize the player's input to prevent moving faster diagonally
        PlayerMovement = new Vector2(RunnerObject.Movement.x, RunnerObject.Movement.y).normalized;
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        // we need a multiplier since we don't want the _speed to be seen like a big
        // number in the inspector. We can also do [SerializeField, Range(250f, 500f)]
        var speedMultiplier = 10;
        RunnerObject.Move(PlayerMovement * (Speed * speedMultiplier * fixedDeltaTime));
    }

    public override void ChangeState()
    {
        if (RunnerObject.RollPressed)
        {
            RunnerObject.SetState(typeof(PlayerRollState));
            return;
        }

        if (PlayerMovement == Vector2.zero)
        {
            RunnerObject.SetState(typeof(PlayerIdleState));
        }
    }
}
