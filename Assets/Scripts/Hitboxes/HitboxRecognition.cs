using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRecognition : MonoBehaviour
{
    private int damage = 0;
    private bool isPlayerHit;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SetDamage(int newDamage) {
        damage = newDamage;
    }

    public void SetIsPlayerHit(bool newIsPlayerHit)
    {
        isPlayerHit = newIsPlayerHit;
    }

    void OnTriggerEnter(Collider other)
    {
            if (isPlayerHit && other.CompareTag("Enemy"))
            {
                Debug.Log("Player Attack");
                HealthSystem healthSystem = other.GetComponent<HealthSystem>();
                healthSystem.Damage(damage);
            }

            else if (!isPlayerHit && other.CompareTag("Player"))
            {
                Debug.Log("Enemy attack");
                player.healthSystem.Damage(damage);
            }
    }
}
