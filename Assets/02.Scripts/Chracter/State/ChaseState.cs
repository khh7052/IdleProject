using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Constants;

public class ChaseState : CharacterState
{
    public ChaseState(CharacterAI character) : base(character) { }
    private Transform chaseTarget;


    public override void Enter()
    {
        base.Enter();
        SetAnimation(AnimatorHash.ChaseHash, true); // �޸��� �ִϸ��̼� ���
        NavmeshController.IsStopped = false; // �̵� ����

        if(character.TeamType == TeamType.Enemy)
            chaseTarget = GameManager.Instance.Player.transform;
    }

    public override void Execute()
    {
        base.Execute();

        if (character.TeamType == TeamType.Player)
        {
            chaseTarget = character.transform.GetNearestTarget(character.SightRange, character.TargetLayerMask);

            if (chaseTarget == null)
            {
                // Ÿ���� ������ ��� ���·� ��ȯ
                character.StateMachine.ChangeState<IdleState>();
                return;
            }
        }

        if (!character.transform.TargetInDistance(chaseTarget, character.SightRange))
        {
            // Ÿ���� �þ� ������ ����� ��� ���·� ��ȯ
            character.StateMachine.ChangeState<IdleState>();
            return;
        }

        // Ÿ���� ���� ���� ���� ������ ���� ���·� ��ȯ
        if (character.transform.TargetInDistance(chaseTarget, character.AttackRange))
            character.StateMachine.ChangeState<AttackState>();
        // Ÿ���� �þ� ������ ������ ����
        else NavmeshController.SetDestination(chaseTarget.position);
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(AnimatorHash.ChaseHash, false); // �޸��� �ִϸ��̼� ���
        chaseTarget = null; // Ÿ�� �ʱ�ȭ
    }

}
