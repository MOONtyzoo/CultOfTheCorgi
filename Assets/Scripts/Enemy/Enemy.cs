using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    /*
     * move around world towards player
     * if within certain distance of player, attack
     * if so damage player
     * if health is depleted destroy enemy
     */
    private void Start()
    {
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (healthSystem.IsHealthDepleted())
        {
            Destroy(gameObject);
        }
    }
}
