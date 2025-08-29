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
        SetAnimation(AnimatorHash.ChaseHash, true); // 달리기 애니메이션 재생
        NavmeshController.IsStopped = false; // 이동 시작

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
                // 타겟이 없으면 대기 상태로 전환
                character.StateMachine.ChangeState<IdleState>();
                return;
            }
        }

        if (!character.transform.TargetInDistance(chaseTarget, character.SightRange))
        {
            // 타겟이 시야 범위를 벗어나면 대기 상태로 전환
            character.StateMachine.ChangeState<IdleState>();
            return;
        }

        // 타겟이 공격 범위 내에 있으면 공격 상태로 전환
        if (character.transform.TargetInDistance(chaseTarget, character.AttackRange))
            character.StateMachine.ChangeState<AttackState>();
        // 타겟이 시야 범위에 있으면 추적
        else NavmeshController.SetDestination(chaseTarget.position);
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(AnimatorHash.ChaseHash, false); // 달리기 애니메이션 재생
        chaseTarget = null; // 타겟 초기화
    }

}
