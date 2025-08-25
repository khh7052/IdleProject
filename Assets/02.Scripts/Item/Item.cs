using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public ItemData data;
    public int level = 1;

    public int Level => level;

    // ���׷��̵� �õ�
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

    // ���� ���� ���� Modifier ���
    public List<StatModifierData> GetModifiers()
    {
        var list = new List<StatModifierData>();
        float multiplier = 1f + data.upgradeValuePerLevel * (level - 1);

        foreach (var mod in data.baseModifiers)
        {
            StatModifierData copy = mod;
            copy.value *= multiplier; // ���� �ݿ�
            copy.source = this;
            list.Add(copy);
        }
        return list;
    }
}
