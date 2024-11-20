using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private HeartContainer heartContainerPrefab;
    private List<HeartContainer> heartContainers = new List<HeartContainer>();

    [SerializeField] private HeartSpriteDictionary heartSpriteDictionary;
    private Dictionary<HeartContainer.State, Sprite> heartSprites = new Dictionary<HeartContainer.State, Sprite>();

    // These two structs below are just to tell Unity how to format data in the inspector
    [Serializable]
    public struct HeartSpriteDictionary {
        public HeartSpriteDictionaryItem[] dictionaryItems;

    }

    [Serializable]
    public struct HeartSpriteDictionaryItem {
        public HeartContainer.State heartState;
        public Sprite heartSprite;
    }
    
    void Awake() {
        InitializeHeartSprites();
        ClearHeartContainers();

        if (healthSystem == null) {
            Debug.Log("This HealthBar is not reading from any HealthSystem!", this);
        } else {
            healthSystem.OnHealthChanged.AddListener(() => UpdateHeartContainers());
        }

        UpdateHeartContainers();
    }

    private void InitializeHeartSprites() {
        // Convert the values set in the inspector to an actual dictionary
        foreach (HeartSpriteDictionaryItem dictionaryItem in heartSpriteDictionary.dictionaryItems) {
            heartSprites.Add(dictionaryItem.heartState, dictionaryItem.heartSprite);
        }
    }

    private void ClearHeartContainers() {
        while (transform.childCount > 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        heartContainers.Clear();
    }

    private void UpdateHeartContainers() {
        CheckIfShouldAddHeartContainers();
        CheckIfShouldRemoveHeartContainers();

        // At this point the amount of container objects is correct
        UpdateHeartContainerSprites();
    }

    private void CheckIfShouldAddHeartContainers() {
        while (heartContainers.Count*2 < healthSystem.GetMaxHealth()) {
            AddHeartContainer();
        }
    }
    
    private void AddHeartContainer() {
        HeartContainer newHeartContainer = Instantiate(heartContainerPrefab, gameObject.transform);
        newHeartContainer.SetID(heartContainers.Count);
        newHeartContainer.PlayAnimation(HeartContainer.AnimationName.Add);
        heartContainers.Add(newHeartContainer);
    }

    private void CheckIfShouldRemoveHeartContainers() {
        while (heartContainers.Count*2 > healthSystem.GetMaxHealth()+1) {
            RemoveHeartContainer();
        }
    }

    private void RemoveHeartContainer() {
        HeartContainer lastHeartContainer = heartContainers[heartContainers.Count-1];
        lastHeartContainer.PlayAnimation(HeartContainer.AnimationName.Remove); // This calls gameobject destroy when done
        heartContainers.Remove(lastHeartContainer);
    }

    private void UpdateHeartContainerSprites() {
        for (int i = 0; i < heartContainers.Count; i++) {
            HeartContainer heartContainer = heartContainers[i];
            HeartContainer.State newState = calculateContainerState(i);

            heartContainer.SetSprite(heartSprites[newState]);
            CheckIfShouldPlayAnimations(heartContainer, newState);
            heartContainer.SetState(newState);
        }
    }

    private HeartContainer.State calculateContainerState(int containerIndex) {
        int containerMaxHealth = Mathf.Clamp(healthSystem.GetMaxHealth() - containerIndex*2, 0, 2);
        int containerHealth = Mathf.Clamp(healthSystem.GetHealth() - containerIndex*2, 0, 2);

        HeartContainer.State heartState;
        // defaults
        heartState.heartType = HeartContainer.HeartType.Red;
        heartState.containerType = HeartContainer.ContainerType.Whole;
        heartState.fillState = HeartContainer.FillState.Full;

        switch(containerMaxHealth) {
            case 0: Debug.Log("This heart container should not exist"); break;
            case 1: heartState.containerType = HeartContainer.ContainerType.Half; break;
            case 2: heartState.containerType = HeartContainer.ContainerType.Whole; break;
        }

        switch(containerHealth) {
            case 0: heartState.fillState = HeartContainer.FillState.Empty; break;
            case 1: heartState.fillState = HeartContainer.FillState.HalfFull; break;
            case 2: heartState.fillState = HeartContainer.FillState.Full; break;
        }

        return heartState;
    }

    private void CheckIfShouldPlayAnimations(HeartContainer heartContainer, HeartContainer.State newState) {
        HeartContainer.State previousState = heartContainer.GetState();

        // Damage animation
        if ((int)newState.fillState < (int)previousState.fillState) {
            print("Heart " + heartContainer.GetID() + " was damaged");
            heartContainer.PlayAnimation(HeartContainer.AnimationName.Damage);
        }

        // Heal animation
        if ((int)newState.fillState > (int)previousState.fillState) {
            print("Heart " + heartContainer.GetID() + " was healed");
            heartContainer.PlayAnimation(HeartContainer.AnimationName.Heal);
        }
    }
}
