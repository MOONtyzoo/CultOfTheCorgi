using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(menuName = "States/Player/Roll")]
public class PlayerRollState : State<Player>
{
    [SerializeField, Range(0f, 50f)] private float RollSpeed = 50f;
    [SerializeField] private float RollTime = .5f;

    [Header("DEBUG")]
    [SerializeField] private bool Debug = true;

    private Vector2 MovementDirection;
    private float ElapsedTime;

    public override void Enter(Player parent)
    {
        base.Enter(parent);

        // grab the direction were the player is aiming in a 3D plane
        Vector2 playerInput = parent.Movement.normalized;

        // instantly set this to false so there's no double rolling
        parent.RollPressed = false;
        ElapsedTime = 0f;
        MovementDirection = playerInput * RollSpeed;

        if (!Debug) return;
        Vector3 startingPos = parent.transform.position;
        float distanceTraveled = RollSpeed * RollTime;
        Vector3 endingPos = startingPos + distanceTraveled * new Vector3(playerInput.x, 0, playerInput.y);
        UnityEngine.Debug.DrawLine(
            startingPos,
            endingPos,
            Color.red,
            .2f
        );
    }

    public override void Tick(float deltaTime)
    {
        ElapsedTime += deltaTime;
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        RunnerObject.SetVelocity(MovementDirection);
    }

    public override void HandleStateTransitions()
    {
        // only change if the "cooldown" timer is reached
        if (ElapsedTime >= RollTime)
        {
            RunnerObject.SetState(typeof(PlayerIdleState));
        }
    }
}
