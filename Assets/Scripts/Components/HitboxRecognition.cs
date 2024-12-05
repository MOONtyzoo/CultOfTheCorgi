using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRecognition : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HitboxRecognition");
            // Damage Enemy
            // Play hit sound maybe?
            // 
        }
    }
}
