using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State<Player>
{
    public override void Enter(Player parent)
    {
        base.Enter(parent);
        RunnerObject.SetAnimation(Player.AnimationName.PlayerIdle);
        RunnerObject.SetVelocity(Vector2.zero);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void FixedTick(float fixedDeltaTime)
    {
    }

    public override void HandleStateTransitions()
    {
        
        if (RunnerObject.movementInput.sqrMagnitude != 0)
        {
            RunnerObject.SetState(typeof(PlayerMoveState));
        }
        
    }
}
