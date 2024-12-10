using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtFlashUI : MonoBehaviour
{
    [SerializeField] private Image flashImage;
    [SerializeField] private HealthSystem healthSystem;

    private float flashTimer;
    [SerializeField] private float flashDuration;
    [SerializeField] private float flashStartOpacity;

    public void Start() {
        healthSystem.OnDamaged.AddListener(StartFlash);
        flashTimer = flashDuration;
    }

    private void StartFlash() {
        flashTimer = 0f;
    }

    private void Update() {
        HandleFlashTimer();
    }

    private void HandleFlashTimer() {
        if (flashTimer <= flashDuration) {
            flashTimer += Time.deltaTime;
            
            float flashOpacity = Mathf.Lerp(flashStartOpacity, 0, GetFlashTimerProgressNormalized());

            Color newColor = flashImage.color;
            newColor.a = flashOpacity;
            flashImage.color = newColor;
        }
    }

    private float GetFlashTimerProgressNormalized() {
        return Mathf.Clamp(flashTimer/flashDuration, 0f, 1f);
    }
}
