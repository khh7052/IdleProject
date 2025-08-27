using Constants;

public class IdleState : CharacterState
{
    public IdleState(CharacterAI character) : base(character) { }

    public override void Enter()
    {
        base.Enter();
        SetAnimation(AnimatorHash.IdleHash, true); // 대기 애니메이션 재생
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

    public override void Exit()
    {
        base.Exit();
        SetAnimation(AnimatorHash.IdleHash, false); // 대기 애니메이션 중지
    }

}