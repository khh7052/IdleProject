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
        RefreshText();
    }

    void Initialize()
    {
        if(target == null)
            target = GameManager.Instance.player;


        foreach (var statType in statTypes)
        {
            var text = Instantiate(textPrefab, textParent);
            textDict.Add(statType, text);
        }
    }

    public void RefreshText()
    {
        foreach (var pair in textDict)
        {
            float value = target.Stats.GetStat(pair.Key).FinalValue;
            pair.Value.text = $"{pair.Key}: {value:F1}";
        }
    }


}
