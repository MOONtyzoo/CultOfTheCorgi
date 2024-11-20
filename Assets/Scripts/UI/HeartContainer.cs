using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    private Image heartImage;
    private Animator animator;
    private State currentState;
    
    [SerializeField, TextArea]
    private string DEBUG_ID;

    [Serializable]
    public struct State {
        public HeartType heartType;
        public ContainerType containerType;
        public FillState fillState;
    }

    public enum HeartType {
        Red,
        Blue
    }

    public enum ContainerType {
        Half,
        Whole,
    }

    public enum FillState {
        Empty,
        HalfFull,
        Full,
    }

    public enum AnimationName {
        Damage,
        Heal,
        Add,
        Remove
    }

    void Awake() {
        heartImage = transform.GetComponentInChildren<Image>();
        animator = GetComponent<Animator>();
    }

    public void SetState(State newState) {
        currentState = newState;
    } 

    public State GetState() {
        return currentState;
    }

    public void SetID(int ID) {
        DEBUG_ID = "ID: " + ID.ToString();
    }

    public string GetID() {
        return DEBUG_ID;
    }
    
    public void SetSprite(Sprite newSprite) {
        heartImage.sprite = newSprite;
    }

    public void PlayAnimation(AnimationName animation) {
        animator.Play(animation.ToString(), -1, 0f);
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
