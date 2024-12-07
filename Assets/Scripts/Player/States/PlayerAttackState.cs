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
    }

    public override void Tick(float deltaTime)
    {
        elapsedTime += deltaTime;

        // If you click the attack button again before the attack ends, then the next attack will play
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
        RunnerObject.SetVelocity(RunnerObject.movementInput * RunnerObject.playerData.movementSpeed * 0.3f);
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
                RunnerObject.FlipSpriteToFaceDirection(RunnerObject.lookInput);
                RunnerObject.hitbox.CreateHitBoxPrefab(RunnerObject.playerData.attack1Damage);
                break;
            case AttackType.Attack2:
                currentAttackDuration = RunnerObject.playerData.attack2Duration;
                RunnerObject.SetAnimation(Player.AnimationName.PlayerAttack2);
                RunnerObject.FlipSpriteToFaceDirection(RunnerObject.lookInput);
                RunnerObject.hitbox.CreateHitBoxPrefab(RunnerObject.playerData.attack2Damage);
                break;
            case AttackType.Attack3:
                currentAttackDuration = RunnerObject.playerData.attack3Duration;
                RunnerObject.SetAnimation(Player.AnimationName.PlayerAttack3);
                RunnerObject.FlipSpriteToFaceDirection(RunnerObject.lookInput);
                RunnerObject.hitbox.CreateHitBoxPrefab(RunnerObject.playerData.attack3Damage);
                break;
        }
        currentAttack = newAttack;
    }
    
}