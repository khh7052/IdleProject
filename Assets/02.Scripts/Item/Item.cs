using System;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[Serializable]
public class Item
{
    public ItemData data;
    public int level = 1;

    public int Level => level;
    public string Name => data.itemName;
    public ItemType Type => data.itemType;

    // ���׷��̵� �õ�
    public bool TryUpgrade(ref float gold)
    {
        float cost = data.GetUpgradeCost(level);
        if (gold >= cost && level < data.maxLevel)
        {
            gold -= cost;
            level++;

            // ���� ����
            CharacterStats stats = GameManager.Instance.player.Stats;
            stats.Unequip(this);
            stats.Equip(this);

            Debug.Log($"Upgraded {Name} to Level {level}");
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
