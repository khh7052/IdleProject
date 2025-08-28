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
        SetAnimation(AnimatorHash.AttackHash, true); // 공격 애니메이션 재생
        NavmeshController.IsStopped = true; // 이동 멈춤

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
            // 타겟이 없으면 대기 상태로 전환
            character.StateMachine.ChangeState<IdleState>();
            return;
        }

        if (character.transform.TargetInDistance(attackTarget, character.AttackRange))
        {
            // 타겟이 공격 범위 내에 있으면 공격
            attackTarget.GetComponent<CharacterAI>()?.TakeDamage(character.Damage);
        }
        else
        {
            // 타겟이 공격 범위를 벗어나면 추적 상태로 전환
            character.StateMachine.ChangeState<ChaseState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(AnimatorHash.AttackHash, false); // 공격 애니메이션 중지
        attackTarget = null; // 타겟 초기화
    }
}
