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

    public void DestroyHitbox()
    {
        if (currentHitboxInstance != null)
        {
            Destroy(currentHitboxInstance);
            currentHitboxInstance = null;
        }
    }

    public void CreateHitBoxPrefab()
    {
        if (currentHitboxInstance == null)
        {
            if (player.IsFacingRight)
            {
                int xOffset = 1;
                float yOffset = -0.5f;
                Vector3 hitboxPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z);
                currentHitboxInstance = Instantiate(hitboxRightPrefab, hitboxPosition, Quaternion.identity);
            }
            else
            {
                int xOffset = -1;
                float yOffset = -0.5f;
                Vector3 hitboxPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z);
                currentHitboxInstance = Instantiate(hitboxLeftPrefab, hitboxPosition, Quaternion.identity);
            }
        }
    }
    
}
