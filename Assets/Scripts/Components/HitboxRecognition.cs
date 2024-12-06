using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRecognition : MonoBehaviour
{
    [SerializeField] private int damage;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HitboxRecognition");
            var healthSystem = other.gameObject.GetComponent<HealthSystem>();
            Debug.Log(damage);
            healthSystem.Damage(damage);
            
        }
    }
    
}
