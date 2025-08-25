using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentState;
    private Dictionary<Type, IState> states = new();
    [SerializeField] private string CurrentState; // 디버깅용

    public void AddState(IState state)
        => states[state.GetType()] = state;

    public void ChangeState<T>() where T : IState
    {
        // 같은 상태로 변경 불가
        if (currentState?.GetType() == typeof(T)) return;

        currentState?.Exit();
        currentState = states[typeof(T)];
        currentState?.Enter();
        CurrentState = currentState.GetType().Name; // 디버깅용
    }

    public void Execute() => currentState?.Execute();
}
