using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corgi : MonoBehaviour
{
    [SerializeField] private SpriteRenderer CorgiSpriteRenderer;
    
    public void Move(Vector2 direction)
    {
        FaceCorrectDirection(direction);
        float xAmount = direction.x * 7f * Time.deltaTime;
        float yAmount = direction.y * 7f * Time.deltaTime;
        
        transform.Translate(xAmount, 0, yAmount);
        
    }
    
    private void FaceCorrectDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            CorgiSpriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            CorgiSpriteRenderer.flipX = true;
        }
    }
}
