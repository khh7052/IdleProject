using UnityEngine;

public class IdleState : CharacterState
{
    public IdleState(CharacterAI character) : base(character) { }

    public override void Enter()
    {
        base.Enter();
        AnimationHandler?.PlayIdle();
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

}