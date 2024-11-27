using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "States/Player/Move")]
public class PlayerMoveState : State<Player>
{
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
        RunnerObject.SetVelocity(PlayerMovement * RunnerObject.playerData.movementSpeed);
    }

    public override void HandleStateTransitions()
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
