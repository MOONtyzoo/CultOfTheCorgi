using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> : ScriptableObject where T : MonoBehaviour
{
    protected T _runner;

    // called whenever we enter this state. Good for setting up variables
    public virtual void Enter(T parent)
    {
        _runner = parent;
    }

    // similar to Update
    public abstract void Tick(float deltaTime);

    // similar to FixedUpdate
    public abstract void FixedTick(float fixedDeltaTime);

    // here we put the conditions to change to another state if needed
    public abstract void ChangeState();

    public virtual void Exit()
    {
    }
}
