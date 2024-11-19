using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    private Image heartImage;
    private HeartState heartState;
    
    [SerializeField, TextArea]
    private string DEBUG_STRING;

    [Serializable]
    public struct HeartState {
        public HeartType heartType;
        public ContainerType containerType;
        public FillState fillState;
    }

    public enum HeartType {
        Red,
        Blue
    }

    public enum ContainerType {
        Whole,
        Half
    }

    public enum FillState {
        Full,
        HalfFull,
        Empty
    }

    void Awake() {
        heartImage = transform.GetComponentInChildren<Image>();
    }

    public void SetID(int ID) {
        DEBUG_STRING = "ID: " + ID.ToString();
    }
    
    public void SetSprite(Sprite newSprite) {
        heartImage.sprite = newSprite;
    }
}
