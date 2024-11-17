using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private Corgi Corgi;
    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector2 direction = new Vector2(0, 1);
            Corgi.Move(direction);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector2 direction = new Vector2(0, -1);
            Corgi.Move(direction);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 direction = new Vector2(-1, 0);
            Corgi.Move(direction);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 direction = new Vector2(1, 0);
            Corgi.Move(direction);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //dodge
        }
        
    }
}
