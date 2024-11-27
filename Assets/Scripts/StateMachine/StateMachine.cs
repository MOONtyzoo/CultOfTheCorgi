using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
    private List<State<T>> States;

    [Header("DEBUG")]
    [SerializeField] private bool Debug = true;

    private State<T> ActiveState;

    private T parent;

    protected virtual void Awake()
    {
        parent = GetComponent<T>();
    }

    protected virtual void Start()
    {
        if (States.Count <= 0) return;

        SetState(States[0]);
    }

    protected void InitializeStateMachine(List<State<T>> States) {
        this.States = States;
    }

    public void SetState(State<T> newStateType)
    {
        ActiveState?.Exit();
        ActiveState = newStateType;
        ActiveState?.Enter(parent);
    }

    public void SetState(Type newStateType)
    {
        State<T> newState = States.FirstOrDefault(s => s.GetType() == newStateType);
        if (newState != null)
        {
            SetState(newState);
        }
    }

    protected virtual void Update()
    {
        ActiveState?.Tick(Time.deltaTime);
        ActiveState?.HandleStateTransitions();
    }

    private void FixedUpdate()
    {
        ActiveState?.FixedTick(Time.fixedDeltaTime);
    }

    private void OnGUI()
    {
        if (!Debug) return;

        var content = ActiveState != null ? nameof(ActiveState) : "(no active state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
