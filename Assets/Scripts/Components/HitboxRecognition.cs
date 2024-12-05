using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRecognition : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HitboxRecognition");
        }
    }
}
