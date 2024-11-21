using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine>
{
    [SerializeField] private PlayerInput _playerInput;
    // this is the Transform we want to rotate on the Y axis when changing directions
    [SerializeField] private SpriteRenderer CorgiSpriteRenderer;
    [SerializeField] private Animator CorgiAnimator;
    [SerializeField] private Rigidbody _rigidbody;

    // this Vector2 can be used on each State to determine any change
    public Vector2 Movement { get; private set; }

    // since our sprite is facing right, we set it to true
    private bool _isFacingRight = true;

    private void OnEnable()
    {
        _playerInput.MovementEvent += HandleMove;
    }

    private void OnDisable()
    {
        _playerInput.MovementEvent -= HandleMove;
    }

    private void HandleMove(Vector2 movement)
    {
        Movement = movement;
        CheckFlipSprite(movement);
    }

    private void CheckFlipSprite(Vector2 velocity)
    {
        if ((!(velocity.x > 0f) || _isFacingRight) && (!(velocity.x < 0f) || !_isFacingRight)) return;

        _isFacingRight = !_isFacingRight;
        CorgiSpriteRenderer.flipX = !CorgiSpriteRenderer.flipX;
    }

    // just  a simple implementation of movement by setting the velocity of the Rigidbody
    public void Move(Vector2 velocity)
    {
        float xMovement = velocity.x * Time.deltaTime;
        float zMovement = velocity.y * 2 * Time.deltaTime;

        Vector3 newPosition = _rigidbody.position + new Vector3(xMovement, 0, zMovement);
        _rigidbody.MovePosition(newPosition);
        _rigidbody.velocity = velocity;
    }

    public void SetAnimation(string animation)
    {
        CorgiAnimator.Play(animation);
    }

}
