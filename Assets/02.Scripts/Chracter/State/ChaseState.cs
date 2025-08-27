using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Constants;

public class ChaseState : CharacterState
{
    public ChaseState(CharacterAI character) : base(character) { }

    public override void Enter()
    {
        base.Enter();
        SetAnimation(AnimatorHash.ChaseHash, true); // �޸��� �ִϸ��̼� ���
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

    public override void Exit()
    {
        base.Exit();
        SetAnimation(AnimatorHash.ChaseHash, false); // �޸��� �ִϸ��̼� ���
    }
}
