using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Roll")]
public class PlayerRollState : State<Player>
{
    [SerializeField] private float RollSpeed = 50f;
    [SerializeField] private float RollTime = .5f;

    [Header("DEBUG")]
    [SerializeField] private bool Debug = true;

    private Vector2 MovementDirection;
    private float ElapsedTime;

    public override void Enter(Player parent)
    {
        base.Enter(parent);

        // grab the direction were the player is aiming in a 3D plane
        var playerInput = new Vector3(parent.Movement.normalized.x, parent.Movement.normalized.y);

        // instantly set this to false so there's no double rolling
        parent.RollPressed = false;

        ElapsedTime = 0f;

        var startingPos = parent.transform.position;

        // calculate the desired end position
        MovementDirection = startingPos + playerInput * RollSpeed;

        if (!Debug) return;

        UnityEngine.Debug.DrawLine(
            startingPos,
            MovementDirection,
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
        if (!(ElapsedTime < RollTime)) return;

        // each fixed frame we move a fraction towards the end value
        RunnerObject.Move(MovementDirection * (ElapsedTime / RollTime));
    }

    public override void ChangeState()
    {
        // only change if the "cooldown" timer is reached
        if (ElapsedTime >= RollTime)
        {
            RunnerObject.SetState(typeof(PlayerIdleState));
        }
    }
}
