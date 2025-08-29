using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Constants;

public class HUD : MonoBehaviour
{
    [SerializeField] private CharacterAI character;

    [SerializeField] private Image hpBar;
    [SerializeField] private Image mpBar;
    [SerializeField] private Image expBar;

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text stageText;
    [SerializeField] private TMP_Text goldText;

    private void Awake()
    {
        GameManager.Instance.StageChanged += OnStageChanged;
        GameManager.Instance.GoldChanged += OnGoldChanged;
        GameManager.Instance.LevelChanged += OnLevelChanged;

        character.Stats.GetResourceStat(StatType.HP).FinalValueChanged += OnHPChanged;
        character.Stats.GetResourceStat(StatType.HP).CurrentValueChanged += OnHPChanged;

        character.Stats.GetResourceStat(StatType.MP).FinalValueChanged += OnMPChanged;
        character.Stats.GetResourceStat(StatType.MP).CurrentValueChanged += OnMPChanged;

        character.Stats.GetResourceStat(StatType.Experience).FinalValueChanged += OnExperienceChanged;
        character.Stats.GetResourceStat(StatType.Experience).CurrentValueChanged += OnExperienceChanged;

        OnHPChanged(0);
        OnMPChanged(0);
        OnExperienceChanged(0);
        OnStageChanged(GameManager.Instance.CurrentStage);
        OnGoldChanged(GameManager.Instance.Gold);
        OnLevelChanged(GameManager.Instance.Level);
    }

    public void OnHPChanged(float _)
    {
        ResourceStat hp = character.Stats.GetResourceStat(StatType.HP);
        hpBar.FillAmount(hp.CurrentValue, hp.FinalValue);
    }

    public void OnMPChanged(float _)
    {
        ResourceStat mp = character.Stats.GetResourceStat(StatType.MP);
        mpBar.FillAmount(mp.CurrentValue, mp.FinalValue);
    }

    public void OnExperienceChanged(float _)
    {
        ResourceStat exp = character.Stats.GetResourceStat(StatType.Experience);
        expBar.FillAmount(exp.CurrentValue, exp.FinalValue);
    }

    public void OnStageChanged(StageData stage) => stageText.text = stage.stageName;
    public void OnGoldChanged(ulong gold) => goldText.text = $"{gold}G";
    public void OnLevelChanged(int level) => levelText.text = $"Lv.{level}";

}
