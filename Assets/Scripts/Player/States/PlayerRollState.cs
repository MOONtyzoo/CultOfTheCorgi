using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(menuName = "States/Player/Roll")]
public class PlayerRollState : State<Player>
{
    [SerializeField, Range(0f, 50f)] private float rollSpeed = 50f;
    [SerializeField] private float rollTime = .5f;

    [Header("DEBUG")]
    [SerializeField] private bool debug = true;

    private Vector2 rollDirection;
    private float elapsedTime;

    public override void Enter(Player parent)
    {
        base.Enter(parent);

        parent.RollPressed = false; // instantly set this to false so there's no double rolling
        elapsedTime = 0f;
        rollDirection = parent.Movement.normalized;

        if (!debug) return;
        Vector3 startingPos = parent.transform.position;
        float distanceTraveled = rollSpeed * rollTime;
        Vector3 endingPos = startingPos + distanceTraveled * new Vector3(rollDirection.x, 0, rollDirection.y);
        UnityEngine.Debug.DrawLine(
            startingPos,
            endingPos,
            Color.red,
            .2f
        );
    }

    public override void Tick(float deltaTime)
    {
        elapsedTime += deltaTime;
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        RunnerObject.SetVelocity(rollDirection*rollSpeed);
    }

    public override void HandleStateTransitions()
    {
        // only change if the "cooldown" timer is reached
        if (elapsedTime >= rollTime)
        {
            RunnerObject.SetState(typeof(PlayerIdleState));
        }
    }
}
