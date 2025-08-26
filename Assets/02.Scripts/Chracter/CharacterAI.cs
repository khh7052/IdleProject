using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavmeshController))]
public class CharacterAI : MonoBehaviour
{
    [SerializeField] private NavmeshController navmeshController;
    [SerializeField] private AnimationHandler animationHandler;
    private StateMachine stateMachine;

    public NavmeshController NavmeshController => navmeshController;
    public AnimationHandler AnimationHandler => animationHandler;
    public StateMachine StateMachine => stateMachine;
    public Transform Target { get; private set; }

    private void Awake()
    {
        if (navmeshController == null)
            navmeshController = GetComponent<NavmeshController>();

        if (animationHandler == null)
            animationHandler = GetComponentInChildren<AnimationHandler>();

        stateMachine = new();

        // ���� ���
        stateMachine.AddState(new IdleState(this));
        stateMachine.AddState(new ChaseState(this));

        // �ʱ� ����
        stateMachine.ChangeState<IdleState>();

        SetTarget(GameManager.Instance.player);
    }

    private void Update()
    {
        stateMachine.Execute();
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}
