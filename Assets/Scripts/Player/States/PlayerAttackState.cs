using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : State<Player>
{
    private float elapsedTime;

    private enum AttackType {
        Attack1,
        Attack2,
        Attack3
    }
    private AttackType currentAttack;
    private float currentAttackDuration;
    private bool hasPlayerInputAttack;

    public override void Enter(Player parent)
    {
        base.Enter(parent);
        RunnerObject.SetVelocity(Vector2.zero);
        SetCurrentAttack(AttackType.Attack1);
        Hit(RunnerObject.transform, true);
        Debug.Log("Attacking");
    }

    public override void Tick(float deltaTime)
    {
        elapsedTime += deltaTime;

        // If you click the attack button again before the attack ends, then the next attack will player
        if (RunnerObject.attackInputDown) {
            hasPlayerInputAttack = true;
        }

        if (elapsedTime >= currentAttackDuration)
        {
            if (hasPlayerInputAttack && HasNextAttack()) {
                GoToNextAttack();
            } else {
                RunnerObject.SetAnimation(Player.AnimationName.PlayerReturnToIdle);
            }
        }
    }
    

    public override void FixedTick(float fixedDeltaTime)
    {
    }

    public override void HandleStateTransitions()
    {
        if (elapsedTime >= currentAttackDuration + 0.1f)
        {
            RunnerObject.SetState(typeof(PlayerIdleState));
        }
    }

    private bool HasNextAttack() {
        return (int)currentAttack <= 2;
    }

    private void GoToNextAttack() {
        SetCurrentAttack(currentAttack + 1);
    }
    
    private void SetCurrentAttack(AttackType newAttack) {
        elapsedTime = 0;
        hasPlayerInputAttack = false;
        switch(newAttack) {
            case AttackType.Attack1:
                currentAttackDuration = RunnerObject.playerData.attack1Duration;
                RunnerObject.SetAnimation(Player.AnimationName.PlayerAttack1);
                break;
            case AttackType.Attack2:
                currentAttackDuration = RunnerObject.playerData.attack2Duration;
                RunnerObject.SetAnimation(Player.AnimationName.PlayerAttack2);
                break;
            case AttackType.Attack3:
                currentAttackDuration = RunnerObject.playerData.attack3Duration;
                RunnerObject.SetAnimation(Player.AnimationName.PlayerAttack3);
                break;
        }
        currentAttack = newAttack;
    }

    public Collider[] Hit(Transform origin, bool isFacingRight)
    {
        var bounds = GetBoundsRelativeToPlayer(origin, isFacingRight);
        bounds.DrawBounds(1);
        return Physics.OverlapBox(bounds.center, bounds.extents);
    }

    private Bounds GetBoundsRelativeToPlayer(Transform player, bool isFacingRight)
    {
        var bounds = new Bounds();
        isFacingRight = true;
        var xValue = isFacingRight ? 1 : -1;
        var offset = new Vector3(1,1,0);
        offset.x *= xValue;
        bounds.center = player.position + offset;
        return bounds;
    }
}