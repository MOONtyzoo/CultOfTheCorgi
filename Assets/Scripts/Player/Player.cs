using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : StateMachine<Player>
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] private InputReader input;

    [SerializeField] private SpriteRenderer PlayerSpriteRenderer;
    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private Rigidbody Rigidbody;

    // These variables are meant to propograte input to the states
    public Vector2 Movement { get; private set; }
    public bool RollPressed;

    protected override void Start()
    {
        List<State<Player>> playerStates = new List<State<Player>>() {
            new PlayerIdleState(),
            new PlayerMoveState(),
            new PlayerRollState(),
        };
        InitializeStateMachine(playerStates);
        base.Start();
    }

    private void OnEnable()
    {
        input.MovementEvent += HandleMove;
        input.RollEvent += HandleRoll;
    }

    private void OnDisable()
    {
        input.MovementEvent -= HandleMove;
        input.RollEvent -= HandleRoll;
    }

    private void HandleRoll()
    {
        RollPressed = true;
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

    public void SetVelocity(Vector2 velocity)
    {
        Vector3 newVelocity =  new Vector3(velocity.x, 0, velocity.y);
        Rigidbody.velocity = newVelocity;
    }

    public void SetAnimation(string animation)
    {
        PlayerAnimator.Play(animation);
    }

}
