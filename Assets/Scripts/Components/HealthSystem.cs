using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    private float health = 100f;
    [SerializeField] private float maxHealth = 100f;

    public UnityEvent OnHealthChanged;
    public UnityEvent OnHealthLost;
    public UnityEvent OnHealedGained;
    public UnityEvent OnHealthDepleted;

    public void Damage(float damageNum) {
        OnHealthLost.Invoke();
        SetHealth(health - damageNum);
    }

    public void Heal(float healingNum) {
        OnHealedGained.Invoke();
        SetHealth(health + healingNum);
    }

    public void SetHealth(float newHealth) {
        newHealth = Mathf.Clamp(newHealth, 0f, maxHealth);
        if (health != newHealth) {
            health = newHealth;
            OnHealthChanged.Invoke();
            if (IsHealthDepleted()) OnHealthDepleted.Invoke();
        }
    }

    public bool IsHealthDepleted() {
        return health == 0;
    }

    public float GetHealth() {
        return health;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public float GetHealthPercentage() {
        return health/maxHealth;
    }

    public override string ToString() {
        return health.ToString("#.0") + "/" + maxHealth.ToString("#.0");
    }
}
