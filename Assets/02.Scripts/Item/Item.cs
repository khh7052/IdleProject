using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public ItemData data;
    public int level = 1;

    public int Level => level;

    // 업그레이드 시도
    public bool TryUpgrade(ref float playerGold)
    {
        float cost = data.GetUpgradeCost(level);
        if (playerGold >= cost && level < data.maxLevel)
        {
            playerGold -= cost;
            level++;
            return true;
        }
        return false;
    }

    // 현재 레벨 기준 Modifier 계산
    public List<StatModifierData> GetModifiers()
    {
        var list = new List<StatModifierData>();
        float multiplier = 1f + data.upgradeValuePerLevel * (level - 1);

        foreach (var mod in data.baseModifiers)
        {
            StatModifierData copy = mod;
            copy.value *= multiplier; // 레벨 반영
            copy.source = this;
            list.Add(copy);
        }
        return list;
    }
}
