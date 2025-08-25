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

    public Stat GetStat(StatType type)
    {
        return stats[type];
    }

    public void AddModifier(StatModifierData modifier)
    {
        GetStat(modifier.statType).AddModifier(modifier);
    }

    public void RemoveModifier(StatModifierData modifier)
    {
        GetStat(modifier.statType).RemoveModifier(modifier);
    }
    public void RemoveModifiersBySource(object source)
    {
        foreach (var stat in stats.Values)
            stat.RemoveModifiersBySource(source);
    }
}
