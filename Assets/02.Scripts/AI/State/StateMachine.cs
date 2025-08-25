using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentState;
    private Dictionary<Type, IState> states = new();
    [SerializeField] private string CurrentState; // ������

    public void AddState(IState state)
        => states[state.GetType()] = state;

    public void ChangeState<T>() where T : IState
    {
        // ���� ���·� ���� �Ұ�
        if (currentState?.GetType() == typeof(T)) return;

        currentState?.Exit();
        currentState = states[typeof(T)];
        currentState?.Enter();
        CurrentState = currentState.GetType().Name; // ������
    }

    public void Execute() => currentState?.Execute();
}
