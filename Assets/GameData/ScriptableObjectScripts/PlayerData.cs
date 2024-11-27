using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="PlayerData", menuName="GameData/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Move State"), Space]
    [SerializeField, Range(0, 50)] public float movementSpeed;

    [Header("Roll State"), Space]
    [SerializeField, Range(0, 50)] public float rollSpeed;
    [SerializeField, Range(0, 1)] public float rollDuration;
}
