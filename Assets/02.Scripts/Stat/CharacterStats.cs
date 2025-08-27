using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    [SerializeField] private Stat[] stats;
    [SerializeField] private ResourceStat[] resourceStats;
    private readonly Dictionary<StatType, Stat> statDict = new();

    public void Initalize()
    {
        foreach (var stat in stats)
            statDict[stat.Type] = stat;

        foreach (var resourceStat in resourceStats)
            statDict[resourceStat.Type] = resourceStat;

        foreach (var stat in statDict.Values)
            stat.Initialize();
    }

    public Stat GetStat(StatType type)
    {
        if (statDict.TryGetValue(type, out var stat))
            return stat;

        return null;
    }
    public ResourceStat GetResourceStat(StatType type)
    {
        if (statDict.TryGetValue(type, out var stat) && stat is ResourceStat resourceStat)
            return resourceStat;

        return null;
    }


    public void AddModifier(StatModifierData modifier)
    {
        GetStat(modifier.statType).AddModifier(modifier);
    }

    public void RemoveModifiersBySource(object source)
    {
        foreach (var stat in statDict.Values)
            stat.RemoveModifiersBySource(source);
    }

    // 아이템 장착 (아이템의 Modifier 적용)
    public void Equip(Item item)
    {
        foreach (var mod in item.GetModifiers())
        {
            AddModifier(mod);
        }
    }

    // 아이템 해제 (아이템의 Modifier 제거)
    public void Unequip(Item item)
    {
        RemoveModifiersBySource(item);
    }
}
