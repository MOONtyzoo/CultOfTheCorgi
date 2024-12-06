using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] Player player;
    [Space]
    [Header("Prefabs")]
    [SerializeField] GameObject hitboxRightPrefab;
    [SerializeField] GameObject hitboxLeftPrefab;
    private GameObject currentHitboxInstance;

    public void CreateHitBoxPrefab(int damage)
    {
        if (currentHitboxInstance == null)
        {
            int xOffset;
            float yOffset;
            if (player.IsFacingRight)
            {
                xOffset = 1;
                yOffset = -0.5f;
            }
            else
            {
                xOffset = -1;
                yOffset = -0.5f;
                
            }
            Vector3 hitboxPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z);
            currentHitboxInstance = Instantiate(hitboxLeftPrefab, hitboxPosition, Quaternion.identity);
            var hitbox = currentHitboxInstance.GetComponent<HitboxRecognition>();
            Destroy(currentHitboxInstance, 0.1f);
            hitbox.SetDamage(damage);
            
        }
    }
    
}
