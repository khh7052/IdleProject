using Constants;
using UnityEngine;

public class IdleState : CharacterState
{
    public IdleState(CharacterAI character) : base(character) { }
    private Transform chaseTarget;

    public override void Enter()
    {
        base.Enter();
        SetAnimation(AnimatorHash.IdleHash, true); // ��� �ִϸ��̼� ���
        NavmeshController.IsStopped = true; // �̵� ����

        if (character.TeamType == TeamType.Enemy)
            chaseTarget = GameManager.Instance.player.transform;
    }

    public override void Execute()
    {
        base.Execute();

        // �÷��̾� ĳ������ ��� �ֺ��� Ÿ���� Ž��
        if (character.TeamType == TeamType.Player)
            chaseTarget = character.transform.GetNearestTarget(character.SightRange, character.TargetLayerMask);

        // Ÿ���� ������ ��� ���� ����
        if (chaseTarget == null) return;

        if (character.transform.TargetInDistance(chaseTarget, character.SightRange))
        {
            // Ÿ���� �þ� ������ ������ ���� ���·� ��ȯ
            ChangeState<ChaseState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(AnimatorHash.IdleHash, false); // ��� �ִϸ��̼� ����
        chaseTarget = null; // Ÿ�� �ʱ�ȭ
    }

}