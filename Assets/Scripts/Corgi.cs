using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Corgi : MonoBehaviour
{
    [SerializeField] private SpriteRenderer CorgiSpriteRenderer;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Animator CorgiAnimator;

    public void Move(Vector2 direction)
    {
        SetAnimation("Running");

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

    public void SetAnimation(string animation)
    {

        if (animation == "Running")
            CorgiAnimator.Play("CorgiRun");
        else if (animation == "Idle")
            CorgiAnimator.Play("CorgiIdle");
    }
}
