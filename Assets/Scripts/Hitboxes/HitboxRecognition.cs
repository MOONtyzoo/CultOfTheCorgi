using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRecognition : MonoBehaviour
{
    private int damage = 0;
    private bool isPlayerHit;

    public void SetDamage(int newDamage) {
        damage = newDamage;
    }

    public void SetIsPlayerHit(bool newIsPlayerHit)
    {
        isPlayerHit = newIsPlayerHit;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isPlayerHit)
            {
                HealthSystem healthSystem = other.GetComponent<HealthSystem>();
                healthSystem.Damage(damage);
            }

            if (!isPlayerHit)
            {
                Debug.Log("Enemy attack");
            }
        }
        
    }
}
