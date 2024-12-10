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

    [SerializeField] private float moveSpeed;

    [SerializeField] private float knockbackSpeed;
    [SerializeField] private float knockbackDuration;

    [SerializeField] private float stunDuration;

    
    private void Awake() {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody>();
        pauseMenuUI = FindObjectOfType<PauseMenuUI>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        currentState = state.Following;
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
        player.addKill();
    }

    private void FollowPlayer() {
        rigidbody.velocity = moveSpeed * GetDirectionToPlayer();
    }

    private Vector3 GetDirectionToPlayer() {
        return (player.transform.position - transform.position).normalized;
    }

    private float GetDistanceToPlayer() {
        return (player.transform.position - transform.position).magnitude;
    }
}
