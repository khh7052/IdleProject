using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class AttackState : CharacterState
{
    public AttackState(CharacterAI character) : base(character) { }
    private Transform attackTarget;
    private float nextAttackTime = 0f;

    public override void Enter()
    {
        base.Enter();
        SetAnimation(AnimatorHash.AttackHash, true); // ���� �ִϸ��̼� ���
        NavmeshController.IsStopped = true; // �̵� ����

        if (character.TeamType == TeamType.Enemy)
            attackTarget = GameManager.Instance.player.transform;
    }

    public override void Execute()
    {
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + character.AttackRate;
        base.Execute();


        if (character.TeamType == TeamType.Player)
            attackTarget = character.transform.GetNearestTarget(character.AttackRange, character.TargetLayerMask);

        if (attackTarget == null)
        {
            // Ÿ���� ������ ��� ���·� ��ȯ
            character.StateMachine.ChangeState<IdleState>();
            return;
        }

        if (character.transform.TargetInDistance(attackTarget, character.AttackRange))
        {
            // Ÿ���� ���� ���� ���� ������ ����
            attackTarget.GetComponent<CharacterAI>()?.TakeDamage(character.Damage);
        }
        else
        {
            // Ÿ���� ���� ������ ����� ���� ���·� ��ȯ
            character.StateMachine.ChangeState<ChaseState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(AnimatorHash.AttackHash, false); // ���� �ִϸ��̼� ����
        attackTarget = null; // Ÿ�� �ʱ�ȭ
    }
}
