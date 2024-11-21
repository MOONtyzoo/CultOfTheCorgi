using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine<Player>
{
    [SerializeField] private PlayerInput PlayerInput;
    // this is the Transform we want to rotate on the Y axis when changing directions
    [SerializeField] private SpriteRenderer PlayerSpriteRenderer;
    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private Rigidbody Rigidbody;

    // this Vector2 can be used on each State to determine any change
    public Vector2 Movement { get; private set; }

    private void OnEnable()
    {
        PlayerInput.MovementEvent += HandleMove;
    }

    private void OnDisable()
    {
        PlayerInput.MovementEvent -= HandleMove;
    }

    private void HandleMove(Vector2 movement)
    {
        Movement = movement;
        CheckFlipSprite(movement);
    }

    private void CheckFlipSprite(Vector2 velocity)
    {
        bool IsFacingRight = !PlayerSpriteRenderer.flipX;

        if ((!(velocity.x > 0f) || IsFacingRight) && (!(velocity.x < 0f) || !IsFacingRight)) return;

        PlayerSpriteRenderer.flipX = !PlayerSpriteRenderer.flipX;
    }

    // just  a simple implementation of movement by setting the velocity of the Rigidbody
    public void Move(Vector2 velocity)
    {
        //CheckFlipSprite(velocity);

        float xMovement = velocity.x * Time.deltaTime;
        float zMovement = velocity.y * 2 * Time.deltaTime;

        Vector3 newPosition = Rigidbody.position + new Vector3(xMovement, 0, zMovement);
        Rigidbody.MovePosition(newPosition);
        Rigidbody.velocity = velocity;
    }

    public void SetAnimation(string animation)
    {
        PlayerAnimator.Play(animation);
    }

}
