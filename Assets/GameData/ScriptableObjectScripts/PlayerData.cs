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

    [Header("Attack State"), Space] 
    [SerializeField, Range(0, 1)] public float attack1Duration;
    [SerializeField, Range(0, 100)] public int attack1Damage;
    [SerializeField, Range(0, 30)] public float attack1MovementPush;
    [SerializeField, Range(0, 1)] public float attack1HitboxSpawnTime;
    [Space]
    [SerializeField, Range(0, 1)] public float attack2Duration;
    [SerializeField, Range(0, 100)] public int attack2Damage;
    [SerializeField, Range(0, 30)] public float attack2MovementPush;
    [SerializeField, Range(0, 1)] public float attack2HitboxSpawnTime;
    [Space]
    [SerializeField, Range(0, 1)] public float attack3Duration;
    [SerializeField, Range(0, 100)] public int attack3Damage;
    [SerializeField, Range(0, 30)] public float attack3MovementPush;
    [SerializeField, Range(0, 1)] public float attack3HitboxSpawnTime;
}
