using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    private int health = 100;
    [SerializeField] private int maxHealth = 100;

    [Space]
    public UnityEvent OnHealthChanged;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealed;
    public UnityEvent OnHealthDepleted;

    void Awake() {
        health = maxHealth;
    }

    public void Damage(int damageNum) {
        OnDamaged.Invoke();
        SetHealth(health - damageNum);
    }

    public void Heal(int healingNum) {
        OnHealed.Invoke();
        SetHealth(health + healingNum);
    }

    public void SetHealth(int newHealth) {
        newHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        if (health != newHealth) {
            health = newHealth;
            OnHealthChanged.Invoke();
            if (IsHealthDepleted()) OnHealthDepleted.Invoke();
        }
    }

    public void SetMaxHealth(int newMaxHealth) {
        newMaxHealth = Mathf.Max(newMaxHealth, 0);
        health = Mathf.Clamp(health, 0, newMaxHealth);
        maxHealth = newMaxHealth;
        OnHealthChanged.Invoke();
    }

    public bool IsHealthDepleted() {
        return health == 0;
    }

    public int GetHealth() {
        return health;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public float GetHealthPercentage() {
        return ((float)health)/maxHealth;
    }

    public override string ToString() {
        return health.ToString() + "/" + maxHealth.ToString();
    }
}
