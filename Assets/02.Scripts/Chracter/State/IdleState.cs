using UnityEngine;

public class IdleState : CharacterState
{
    public IdleState(CharacterAI character) : base(character) { }

    public override void Enter()
    {
        base.Enter();
        AnimationHandler?.PlayIdle();
        NavmeshController.IsStopped = true; // 이동 멈춤
    }

    public override void Execute()
    {
        base.Execute();

        // 대기 상태에서의 로직 구현 (예: 주변 탐색)
        if (character.Target != null)
        {
            // 타겟이 있으면 추적 상태로 전환
            character.StateMachine.ChangeState<ChaseState>();
        }
    }

}