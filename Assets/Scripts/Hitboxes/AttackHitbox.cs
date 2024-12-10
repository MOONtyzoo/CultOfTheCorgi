using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] Player player;
    private HitboxRecognition hitboxRecognition;
    [Header("Player Prefabs"), Space]
    [SerializeField] GameObject hitboxPrefab;
    [SerializeField] Transform leftHitboxSpawnpoint;
    [SerializeField] Transform rightHitboxSpawnpoint;
    private GameObject currentPlayerHitboxInstance;

    [Header("Enemy Prefabs"), Space] 
    [SerializeField] private GameObject enemyHitboxPrefab;
    [SerializeField] private Transform enemyHitboxSpawnPoint;
    private GameObject currentEnemyHitboxInstance;

    public void CreateHitBoxPrefab(int damage, bool isPlayer)
    {
        if (isPlayer && currentPlayerHitboxInstance == null)
        {
            Transform hitboxSpawnPoint;
            if (player.IsFacingRight)
            {
                hitboxSpawnPoint = rightHitboxSpawnpoint;
            }
            else
            {
                hitboxSpawnPoint = leftHitboxSpawnpoint;
            }

            currentPlayerHitboxInstance = Instantiate(hitboxPrefab, hitboxSpawnPoint);
            hitboxRecognition = currentPlayerHitboxInstance.GetComponent<HitboxRecognition>();
            hitboxRecognition.SetIsPlayerHit(true);
            hitboxRecognition.SetDamage(damage);
            Destroy(currentPlayerHitboxInstance, 0.1f);
        }

        else if (currentEnemyHitboxInstance == null && isPlayer == false)
        {
            currentEnemyHitboxInstance = Instantiate(enemyHitboxPrefab, enemyHitboxSpawnPoint);
            hitboxRecognition = currentEnemyHitboxInstance.GetComponent<HitboxRecognition>();
            hitboxRecognition.SetIsPlayerHit(false);
            Destroy(currentEnemyHitboxInstance, 0.1f);
        }
    }

}
