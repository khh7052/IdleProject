using Constants;

public class IdleState : CharacterState
{
    public IdleState(CharacterAI character) : base(character) { }

    public override void Enter()
    {
        base.Enter();
        SetAnimation(AnimatorHash.IdleHash, true); // ��� �ִϸ��̼� ���
        NavmeshController.IsStopped = true; // �̵� ����
    }

    public override void Execute()
    {
        base.Execute();

        // ��� ���¿����� ���� ���� (��: �ֺ� Ž��)
        if (character.Target != null)
        {
            // Ÿ���� ������ ���� ���·� ��ȯ
            character.StateMachine.ChangeState<ChaseState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(AnimatorHash.IdleHash, false); // ��� �ִϸ��̼� ����
    }

}