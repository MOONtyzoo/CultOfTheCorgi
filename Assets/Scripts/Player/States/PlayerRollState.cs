using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.Interactions;

public class PlayerRollState : State<Player>
{
    [Header("DEBUG")]
    [SerializeField] private bool debug = true;

    private Vector2 rollDirection;
    private float elapsedTime;

    public override void Enter(Player parent)
    {
        base.Enter(parent);

        elapsedTime = 0f;
        RunnerObject.SetAnimation(Player.AnimationName.PlayerRoll);
        rollDirection = parent.movementInput;

        if (!debug) return;
        DebugDrawRollLine();
    }

    public override void Tick(float deltaTime)
    {
        elapsedTime += deltaTime;
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        RunnerObject.SetVelocity(rollDirection*RunnerObject.playerData.rollSpeed);
    }

    public override void HandleStateTransitions()
    {
        // only change if the "cooldown" timer is reached
        if (elapsedTime >= RunnerObject.playerData.rollDuration)
        {
            RunnerObject.SetState(typeof(PlayerIdleState));
        }
        
        if (RunnerObject.attackInputDown)
        {
            RunnerObject.SetState(typeof(PlayerAttackState));
            return;
        }
    }

    public void DebugDrawRollLine() {
        Vector3 startingPos = RunnerObject.transform.position;
        float distanceTraveled = RunnerObject.playerData.rollSpeed * RunnerObject.playerData.rollDuration;
        Vector3 endingPos = startingPos + distanceTraveled * new Vector3(rollDirection.x, 0, rollDirection.y);
        UnityEngine.Debug.DrawLine(
            startingPos,
            endingPos,
            Color.red,
            .2f
        );
    }
}
