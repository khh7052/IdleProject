using Constants;
using UnityEngine;

public class IdleState : CharacterState
{
    public IdleState(CharacterAI character) : base(character) { }
    private Transform chaseTarget;

    public override void Enter()
    {
        base.Enter();
        SetAnimation(AnimatorHash.IdleHash, true); // 대기 애니메이션 재생
        NavmeshController.IsStopped = true; // 이동 멈춤

        if (character.TeamType == TeamType.Enemy)
            chaseTarget = GameManager.Instance.player.transform;
    }

    public override void Execute()
    {
        base.Execute();

        // 플레이어 캐릭터인 경우 주변의 타겟을 탐색
        if (character.TeamType == TeamType.Player)
            chaseTarget = character.transform.GetNearestTarget(character.SightRange, character.TargetLayerMask);

        // 타겟이 없으면 대기 상태 유지
        if (chaseTarget == null) return;

        if (character.transform.TargetInDistance(chaseTarget, character.SightRange))
        {
            // 타겟이 시야 범위에 있으면 추적 상태로 전환
            ChangeState<ChaseState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(AnimatorHash.IdleHash, false); // 대기 애니메이션 중지
        chaseTarget = null; // 타겟 초기화
    }

}