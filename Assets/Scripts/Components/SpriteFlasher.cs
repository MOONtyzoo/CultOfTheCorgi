using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/*
    If you are using this script and it is not working, make sure
    that the material of the sprite is set to the SpriteEffects material.
*/

[RequireComponent(typeof(SpriteEffectsPropertySetter))]
public class SpriteFlasher : MonoBehaviour
{
    [SerializeField] private Color32 flashColor = Color.white;
    private SpriteEffectsPropertySetter spriteEffectsPropertySetter;

    Sequence hitFlashSequence;

    public void Awake() {
        spriteEffectsPropertySetter = GetComponent<SpriteEffectsPropertySetter>();
    }

    public void SingleFlash(float duration) {
        hitFlashSequence?.Kill();
        hitFlashSequence = DOTween.Sequence();

        spriteEffectsPropertySetter?.SetTintColor(flashColor);
        spriteEffectsPropertySetter?.SetTintAmount(1.0f);

        Tween hitFlashTween = DOTween.To((x) => {spriteEffectsPropertySetter?.SetTintAmount(x);}, 1.0f, 0.0f, duration);
        hitFlashSequence.Append(hitFlashTween);
    }

    public void ChargeUpFlash(float duration, float flashOpacity) {
        hitFlashSequence?.Kill();
        hitFlashSequence = DOTween.Sequence();

        spriteEffectsPropertySetter?.SetTintColor(flashColor);
        spriteEffectsPropertySetter?.SetTintAmount(0.0f);

        Tween hitFlashTween = DOTween.To((x) => {spriteEffectsPropertySetter?.SetTintAmount(x);}, 0.0f, flashOpacity, duration);
        hitFlashSequence.Append(hitFlashTween);
        hitFlashTween.onComplete += () => spriteEffectsPropertySetter?.SetTintAmount(0.0f);
    }

    public void MultiFlash(float flashAmount, float duration) {
        hitFlashSequence?.Kill();
        hitFlashSequence = DOTween.Sequence();

        for (int i = 0; i < flashAmount; i++) {
            spriteEffectsPropertySetter?.SetTintColor(flashColor);
            spriteEffectsPropertySetter?.SetTintAmount(1.0f);
            Tween hitFlashTween = DOTween.To((x) => {spriteEffectsPropertySetter?.SetTintAmount(x);}, 1.0f, 0.0f, duration/flashAmount);
            hitFlashSequence.Append(hitFlashTween);
        }
    }

    public void OnDestroy() {
        hitFlashSequence.Kill();
    }
}
