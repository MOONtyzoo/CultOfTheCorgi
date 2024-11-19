using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private HeartContainer heartContainerPrefab;
    private List<HeartContainer> heartContainers = new List<HeartContainer>();

    [SerializeField] private HeartSpriteDictionary heartSpriteDictionary;
    private Dictionary<HeartContainer.HeartState, Sprite> heartSprites = new Dictionary<HeartContainer.HeartState, Sprite>();

    // These two structs below are just to tell Unity how to format data in the inspector
    [Serializable]
    public struct HeartSpriteDictionary {
        public HeartSpriteDictionaryItem[] dictionaryItems;

    }

    [Serializable]
    public struct HeartSpriteDictionaryItem {
        public HeartContainer.HeartState heartState;
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
        heartContainers.Add(newHeartContainer);
    }

    private void CheckIfShouldRemoveHeartContainers() {
        while (heartContainers.Count*2 > healthSystem.GetMaxHealth()+1) {
            RemoveHeartContainer();
        }
    }

    private void RemoveHeartContainer() {
        HeartContainer lastHeartContainer = heartContainers[heartContainers.Count-1];
        Destroy(lastHeartContainer.gameObject);
        heartContainers.Remove(lastHeartContainer);
    }

    private void UpdateHeartContainerSprites() {
        for (int i = 0; i < heartContainers.Count; i++) {
            HeartContainer heartContainer = heartContainers[i];
            Sprite sprite = calculateHeartContainerSprite(i);
            heartContainer.SetSprite(sprite);
        }
    }

    private Sprite calculateHeartContainerSprite(int containerIndex) {
        int containerMaxHealth = Mathf.Clamp(healthSystem.GetMaxHealth() - containerIndex*2, 0, 2);
        int containerHealth = Mathf.Clamp(healthSystem.GetHealth() - containerIndex*2, 0, 2);

        HeartContainer.HeartState heartState;
        // defaults
        heartState.heartType = HeartContainer.HeartType.Red;
        heartState.containerType = HeartContainer.ContainerType.Whole;
        heartState.fillState = HeartContainer.FillState.Full;

        switch(containerMaxHealth) {
            case 0: Debug.Log("This heart container should not exist"); return null;
            case 1: heartState.containerType = HeartContainer.ContainerType.Half; break;
            case 2: heartState.containerType = HeartContainer.ContainerType.Whole; break;
        }

        switch(containerHealth) {
            case 0: heartState.fillState = HeartContainer.FillState.Empty; break;
            case 1: heartState.fillState = HeartContainer.FillState.HalfFull; break;
            case 2: heartState.fillState = HeartContainer.FillState.Full; break;
        }

        return heartSprites[heartState];
    }
}
