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
        NavmeshController.IsStopped = false; // 이동 시작
    }

    public override void Execute()
    {
        base.Execute();
        // 추적 로직 구현 (예: 플레이어 위치로 이동)
        if (character.Target == null)
        {
            // 타겟이 없으면 대기 상태로 전환
            character.StateMachine.ChangeState<IdleState>();
            return;
        }

        NavmeshController.SetDestination(character.Target.position);
    }
}
