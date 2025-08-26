using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChaseState : CharacterState
{
    public ChaseState(CharacterAI character) : base(character) { }

    public override void Enter()
    {
        base.Enter();
        AnimationHandler?.PlayChase();
        NavmeshController.IsStopped = false; // �̵� ����
    }

    public override void Execute()
    {
        base.Execute();
        // ���� ���� ���� (��: �÷��̾� ��ġ�� �̵�)
        if (character.Target == null)
        {
            // Ÿ���� ������ ��� ���·� ��ȯ
            character.StateMachine.ChangeState<IdleState>();
            return;
        }

        NavmeshController.SetDestination(character.Target.position);
    }
}
