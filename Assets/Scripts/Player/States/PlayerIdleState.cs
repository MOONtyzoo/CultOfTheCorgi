using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Idle")]
public class PlayerIdleState : State<Player>
{
    public override void Enter(Player parent)
    {
        base.Enter(parent);
        RunnerObject.SetAnimation("PlayerIdle");
        RunnerObject.Move(Vector2.zero);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void FixedTick(float fixedDeltaTime)
    {
    }

    public override void HandleStateTransitions()
    {
        if (RunnerObject.Movement.sqrMagnitude != 0)
        {
            RunnerObject.SetState(typeof(PlayerMoveState));
        }
    }
}
