using System.Collections.Generic;
using Constants;

public class CharacterStats
{
    private readonly Dictionary<StatType, Stat> stats = new();

    public void AddStat(Stat stat)
    {
        if (!stats.ContainsKey(stat.Type))
            stats.Add(stat.Type, stat);
    }

    public Stat GetStat(StatType type) => stats[type];

    public void AddModifier(StatModifierData modifier)
    {
        GetStat(modifier.statType).AddModifier(modifier);
    }

    public void RemoveModifiersBySource(object source)
    {
        foreach (var stat in stats.Values)
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
