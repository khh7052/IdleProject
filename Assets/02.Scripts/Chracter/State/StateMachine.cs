using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public event Action<Type> OnStateChanged;

    private IState currentState;
    private readonly Dictionary<Type, IState> states = new();
    [SerializeField] private string CurrentState; // ������

    public void AddState(IState state)
        => states[state.GetType()] = state;

    public void ChangeState<T>() where T : IState
    {
        var stateType = typeof(T);
        // ���� ���·� ���� �Ұ�
        if (currentState?.GetType() == stateType) return;

        currentState?.Exit();
        currentState = states[typeof(T)];
        currentState?.Enter();
        CurrentState = currentState.GetType().Name; // ������
        OnStateChanged?.Invoke(stateType);
    }

    public void Execute() => currentState?.Execute();
}
