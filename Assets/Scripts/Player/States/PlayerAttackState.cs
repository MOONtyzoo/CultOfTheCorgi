using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : State<Player>
{
    private float elapsedTime;
    public override void Enter(Player parent)
    {
        base.Enter(parent);
        elapsedTime = 0f;
        RunnerObject.SetVelocity(Vector2.zero);
        RunnerObject.SetAnimation("PlayerIdle");
        Debug.Log("Attacking");
    }

    public override void Tick(float deltaTime)
    {
        elapsedTime += deltaTime;
    }
    

    public override void FixedTick(float fixedDeltaTime)
    {
    }

    public override void HandleStateTransitions()
    {
        
        if (elapsedTime >= RunnerObject.playerData.attackDuration)
        {
            RunnerObject.SetState(typeof(PlayerIdleState));
        }
    }
    
}