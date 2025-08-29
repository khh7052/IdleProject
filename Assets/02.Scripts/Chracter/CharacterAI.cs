using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Constants;
using System;

[RequireComponent(typeof(NavmeshController))]
public class CharacterAI : MonoBehaviour
{
    public event Action DieAction;

    [SerializeField] private CharacterStats stats;
    [SerializeField] private TeamType teamType;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private NavmeshController navmeshController;
    [SerializeField] private AnimationHandler animationHandler;
    [SerializeField] private Image hpBar;
    [SerializeField] private bool useGizmo;

    private StateMachine stateMachine;

    public CharacterStats Stats
    {
        get
        {
            if(!stats.IsInitialized)
                stats.Initalize();

            return stats;
        }
    }

    public TeamType TeamType => teamType;
    public LayerMask TargetLayerMask => targetLayerMask;
    public NavmeshController NavmeshController => navmeshController;
    public AnimationHandler AnimationHandler => animationHandler;
    public StateMachine StateMachine => stateMachine;

    public float HP => stats.GetResourceStat(StatType.HP).CurrentValue;
    public float MaxHP => stats.GetResourceStat(StatType.HP).FinalValue;
    public float MP => stats.GetResourceStat(StatType.MP).CurrentValue;
    public float MaxMP => stats.GetResourceStat(StatType.MP).FinalValue;
    public float MoveSpeed => stats.GetStat(StatType.MoveSpeed).FinalValue;

    public float SightRange => stats.GetStat(StatType.SightRange).FinalValue;
    public float Damage => stats.GetStat(StatType.Damage).FinalValue;
    public float AttackRange => stats.GetStat(StatType.AttackRange).FinalValue;
    public float AttackRate => stats.GetStat(StatType.AttackRate).FinalValue;
    public float Defense => stats.GetStat(StatType.Defense).FinalValue;

    public float Experience => stats.GetResourceStat(StatType.Experience).FinalValue;
    public float ExperienceOnDeath => stats.GetStat(StatType.ExperienceOnDeath).FinalValue;
    public float Gold => stats.GetStat(StatType.Gold).FinalValue;


    private void Awake()
    {
        stats ??= new();
        navmeshController ??= GetComponent<NavmeshController>();
        animationHandler ??= GetComponentInChildren<AnimationHandler>();
        stateMachine = new();

        // ĳ���� ���� �ʱ�ȭ
        stats.Initalize();

        // ���� ���
        stateMachine.AddState(new IdleState(this));
        stateMachine.AddState(new ChaseState(this));
        stateMachine.AddState(new AttackState(this));

        // �ʱ� ����
        stateMachine.ChangeState<IdleState>();

        // �̵� �ӵ� �ʱ�ȭ
        navmeshController.MoveSpeed = MoveSpeed;
        stats.GetStat(StatType.MoveSpeed).FinalValueChanged += (value) => navmeshController.MoveSpeed = value;
    }

    private void Update()
    {
        stateMachine.Execute();
    }

    public void Initialize()
    {
        ResourceStat hp = stats.GetResourceStat(StatType.HP);
        hp.Initialize();
    }

    public void TakeDamage(float damage)
    {
        // ���� ����
        damage -= Defense;

        // ������ ����
        stats.GetResourceStat(StatType.HP).Reduce(damage);

        // HP�� ����
        if (hpBar != null)
            hpBar.fillAmount = HP / MaxHP;

        // ��� üũ
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ���");
        gameObject.SetActive(false);

        // �߰����� ��� ó�� (��: ����ġ ȹ��, ������ ��� ��)
        if (teamType == TeamType.Enemy)
        {
            // �÷��̾�� ����ġ�� ��� ����
            var player = GameManager.Instance.Player;
            GameManager.Instance.AddExperience(ExperienceOnDeath);
            GameManager.Instance.AddGold((ulong)Gold);
        }

        DieAction?.Invoke();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (!useGizmo) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, SightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
