using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using TMPro;

public class InformationUI : MonoBehaviour
{
    [SerializeField] private StatType[] statTypes;
    [SerializeField] private CharacterAI target;
    [SerializeField] private RectTransform textParent;
    [SerializeField] private TMP_Text textPrefab;

    private Dictionary<StatType, TMP_Text> textDict = new();

    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        if(target == null)
            target = GameManager.Instance.Player;

        foreach (var statType in statTypes)
        {
            var text = Instantiate(textPrefab, textParent);
            textDict.Add(statType, text);

            Stat stat = target.Stats.GetStat(statType);
            stat.IsDirtyChanged += () => RefreshText(statType);
            RefreshText(statType);
        }
    }

    public void RefreshText(StatType statType)
    {
        if (textDict.TryGetValue(statType, out var text))
        {
            float value = target.Stats.GetStat(statType).FinalValue;
            Debug.Log($"Refresh {statType} : {value}");
            text.text = $"{statType}: {value:F2}";
        }
    }


}
