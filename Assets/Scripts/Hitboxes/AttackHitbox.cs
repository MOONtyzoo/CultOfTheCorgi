using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] Player player;
    [Space]
    [Header("Prefabs")]
    [SerializeField] GameObject hitboxPrefab;
    [SerializeField] Transform leftHitboxSpawnpoint;
    [SerializeField] Transform rightHitboxSpawnpoint;
    private GameObject currentHitboxInstance;

    public void CreateHitBoxPrefab(int damage)
    {
        if (currentHitboxInstance == null)
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

            currentHitboxInstance = Instantiate(hitboxPrefab, hitboxSpawnPoint);
            HitboxRecognition hitboxRecognition = currentHitboxInstance.GetComponent<HitboxRecognition>();
            hitboxRecognition.SetDamage(damage);
            Destroy(currentHitboxInstance, 0.1f);
        }
    }
    
}
