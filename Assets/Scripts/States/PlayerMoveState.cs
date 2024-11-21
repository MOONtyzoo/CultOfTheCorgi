using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Move")]
public class PlayerMoveState : State<PlayerStateMachine>
{
    [SerializeField, Range(0f, 50f)] private float _speed = 25f;

    private Vector2 _playerInput;

    public override void Enter(PlayerStateMachine parent)
    {
        base.Enter(parent);
        _runner.SetAnimation("CorgiRun");
    }

    public override void Tick(float deltaTime)
    {
        _playerInput = _runner.Movement;
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        // we need a multiplier since we don't want the _speed to be seen like a big
        // number in the inspector. We can also do [SerializeField, Range(250f, 500f)]
        var speedMultiplier = 10;
        _runner.Move(_playerInput * (_speed * speedMultiplier * fixedDeltaTime));
    }

    public override void ChangeState()
    {
        if (_playerInput.sqrMagnitude <= .1f)
        {
            _runner.SetState(typeof(PlayerIdleState));
        }
    }
}
