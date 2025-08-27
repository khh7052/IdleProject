using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    [SerializeField] private Stat[] stats;
    private readonly Dictionary<StatType, Stat> statDict = new();

    public void Initalize()
    {
        foreach (var stat in stats)
            statDict[stat.Type] = stat;
    }

    public Stat GetStat(StatType type) => statDict[type];

    public void AddModifier(StatModifierData modifier)
    {
        GetStat(modifier.statType).AddModifier(modifier);
    }

    public void RemoveModifiersBySource(object source)
    {
        foreach (var stat in statDict.Values)
            stat.RemoveModifiersBySource(source);
    }

    // ������ ���� (�������� Modifier ����)
    public void Equip(Item item)
    {
        foreach (var mod in item.GetModifiers())
        {
            AddModifier(mod);
        }
    }

    // ������ ���� (�������� Modifier ����)
    public void Unequip(Item item)
    {
        RemoveModifiersBySource(item);
    }
}
