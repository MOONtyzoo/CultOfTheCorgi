using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : StateMachine<Player>
{
    [SerializeField] public PlayerData playerData;
    [SerializeField] private InputReader input;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private new Rigidbody rigidbody;

    // These variables propagate input to the states
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public bool rollInputDown; // Follows convention of Input.GetKeyDown: Only true on first frame of input

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        List<State<Player>> playerStates = new List<State<Player>>() {
            new PlayerIdleState(),
            new PlayerMoveState(),
            new PlayerRollState(),
        };
        InitializeStateMachine(playerStates);
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

    protected override void Update()
    {
        base.Update();
        rollInputDown = false;
    }

    public void FlipSpriteToFaceDirection(Vector2 direction)
    {
        if (direction.x == 0) return;
        spriteRenderer.flipX = direction.x < 0f;
    }

    public void SetVelocity(Vector2 velocity)
    {
        Vector3 newVelocity =  new Vector3(velocity.x, 0, velocity.y);
        rigidbody.velocity = newVelocity;
    }

    public void SetAnimation(string animation)
    {
        animator.Play(animation);
    }

    // ---------- Event Listeners ---------- //

    private void HandleRoll()
    {
        rollInputDown = true;
    }

    private void HandleMove(Vector2 movement)
    {
        movementInput = movement;
    }

}
