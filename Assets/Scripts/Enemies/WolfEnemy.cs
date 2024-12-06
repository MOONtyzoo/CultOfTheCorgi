using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthSystem))]
public class WolfEnemy : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Awake() {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start() {
        healthSystem.OnHealthDepleted.AddListener(Die);
    }

    private void Die()
    {
        // Maybe play a death animation or particles or something
        Destroy(gameObject);
    }
}
