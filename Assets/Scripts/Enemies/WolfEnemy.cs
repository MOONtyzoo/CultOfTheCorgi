using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(HealthSystem))]
public class WolfEnemy : MonoBehaviour
{
    private HealthSystem healthSystem;
    private new Rigidbody rigidbody;
    private Player player;
    private PauseMenuUI pauseMenuUI;
    private bool isRanged;
    private AttackHitbox attackHitbox;
    private SpriteRenderer enemySpriteRenderer;

    private state currentState = state.Idle;
    private enum state {
        Idle,
        Following,
        Knockback,
        Stunned,
        Attacking,
        Paused
    }

    [SerializeField] private float detectionRadius;
    [SerializeField] private float disengageRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private float disengageAttackRadius;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float knockbackSpeed;
    [SerializeField] private float knockbackDuration;

    [SerializeField] private float stunDuration;
    [SerializeField] private GameObject hitboxSpawnPoint;

    private int enemyAttackDamage = 1;

    
    private void Awake() {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody>();
        pauseMenuUI = FindObjectOfType<PauseMenuUI>();
        attackHitbox = FindObjectOfType<AttackHitbox>();
        enemySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start() {
        healthSystem.OnHealthDepleted.AddListener(Die);
        healthSystem.OnDamaged.AddListener(OnHit);
    }
    
    private void DisableEnemyMovement()
    {
        rigidbody.velocity = Vector3.zero;
        if (!pauseMenuUI.isPaused)
        {
            currentState = state.Idle;
        }
    }
    
    private void Update() {
        if (currentState == state.Following) {
            FollowPlayer();
        }

        if (currentState == state.Idle) {
            rigidbody.velocity = Vector3.zero;
        }

        if (currentState == state.Paused)
        {
            DisableEnemyMovement();
        }

        if (currentState == state.Attacking)
        {
            rigidbody.velocity = Vector3.zero;
        }

        UpdateState();
        //print(rigidbody.velocity);
    }

    private void UpdateState() {
        if (currentState == state.Idle) {
            if (GetDistanceToPlayer() < detectionRadius) {
                currentState = state.Following;
            }
        }

        if (currentState == state.Following) {
            if (GetDistanceToPlayer() > disengageRadius) {
                currentState = state.Idle;
            }

            if (GetDistanceToPlayer() < attackRadius)
            {
                currentState = state.Attacking;
                StartCoroutine(attackCoroutine());
            }
        }

        if (pauseMenuUI.isPaused)
        {
            currentState = state.Paused;
        }
    }

    private void OnHit() {
        currentState = state.Knockback;
        StartCoroutine(knockbackCoroutine());
    }

    private IEnumerator attackCoroutine()
    {
        attackHitbox.CreateHitBoxPrefab(enemyAttackDamage, false, hitboxSpawnPoint);
        if (GetDistanceToPlayer() > disengageAttackRadius)
        {
            currentState = state.Following;
        }
        yield return new WaitForSeconds(2f);
        currentState = state.Idle;
    }
    
    private IEnumerator knockbackCoroutine() {
        float knockbackTimer = 0;
        while (knockbackTimer < knockbackDuration) {
            knockbackTimer += Time.deltaTime;

            Vector3 knockbackDirection = -GetDirectionToPlayer();
            float normalizedTime = knockbackTimer/knockbackDuration;
            rigidbody.velocity = knockbackDirection * Mathf.Lerp(knockbackSpeed, 0.0f, normalizedTime);

            yield return null;
        }

        currentState = state.Stunned;
        yield return new WaitForSeconds(stunDuration);
        currentState = state.Idle;
    }

    private void Die()
    {
        // Maybe play a death animation or particles or something
        Destroy(gameObject);
    }

    private void FollowPlayer() {
        rigidbody.velocity = moveSpeed * GetDirectionToPlayer();
        FlipSpriteToFaceDirection(GetDirectionToPlayer());
    }

    private Vector3 GetDirectionToPlayer() {
        return (player.transform.position - transform.position).normalized;
    }

    private float GetDistanceToPlayer() {
        return (player.transform.position - transform.position).magnitude;
    }
    
    public void FlipSpriteToFaceDirection(Vector2 direction)
    {
        if (direction.x == 0) return;
        enemySpriteRenderer.flipX = direction.x < 0f;
    }
}
