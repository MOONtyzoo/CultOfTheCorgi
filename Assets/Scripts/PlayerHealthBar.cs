using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private GameObject heartContainerPrefab;
    List<GameObject> heartContainers;
    
    void Awake() {
        if (healthSystem == null) {
            Debug.Log("This HealthBar is not reading from any HealthSystem!", this);
        } else {
            // Connect event listeners here
        }
    }

    
}
