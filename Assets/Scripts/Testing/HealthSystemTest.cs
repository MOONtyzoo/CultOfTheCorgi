using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemTest : MonoBehaviour
{
    private HealthSystem healthSystem;

    void Awake() {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamaged.AddListener(() => OnDamaged());
        healthSystem.OnHealed.AddListener(() => OnHealed());
        healthSystem.OnHealthChanged.AddListener(() => OnHealthChanged());
        healthSystem.OnHealthDepleted.AddListener(() => OnHealthDepleted());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            healthSystem.Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            healthSystem.Heal(10);
        }
    }

    public void OnDamaged() {
        print("Took damage!");
    }

    public void OnHealed() {
        print("Healed!");
    }

    public void OnHealthChanged() {
        print("Health changed: " + healthSystem.ToString());
    }

    public void OnHealthDepleted() {
        print("Health depleted! Character is dead");
        Destroy(gameObject);
    }
}
