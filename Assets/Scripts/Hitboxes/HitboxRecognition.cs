using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRecognition : MonoBehaviour
{
    private int damage = 0;

    public void SetDamage(int newDamage) {
        damage = newDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            healthSystem.Damage(damage);
        }
    }
}
