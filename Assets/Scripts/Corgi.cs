using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corgi : MonoBehaviour
{
    [SerializeField] private SpriteRenderer CorgiSpriteRenderer;
    [SerializeField] private Rigidbody rigidBody;
    
    public void Move(Vector2 direction)
    {
        FaceCorrectDirection(direction);
        float xMovement = direction.x * 7f * Time.deltaTime;
        float zMovement = direction.y * 7f * Time.deltaTime;
        
        Vector3 newPosition = rigidBody.position + new Vector3(xMovement, 0, zMovement);
        rigidBody.MovePosition(newPosition);
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
