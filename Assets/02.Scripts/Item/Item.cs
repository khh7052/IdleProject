using System;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[Serializable]
public class Item
{
    public ItemData data;

    public int Level => data.level;
    public string Name => data.itemName;
    public ItemType Type => data.itemType;

    // ���׷��̵� �õ�
    public bool TryUpgrade(ref float gold)
    {
        float cost = data.GetUpgradeCost();
        if (gold >= cost && Level < data.maxLevel)
        {
            gold -= cost;
            data.level++;

            // ���� ����
            CharacterStats stats = GameManager.Instance.Player.Stats;
            stats.Unequip(this);
            stats.Equip(this);

            Debug.Log($"Upgraded {Name} to Level {Level}");
            return true;
        }
        return false;
    }

    // ���� ���� ���� Modifier ���
    public List<StatModifierData> GetModifiers()
    {
        var list = new List<StatModifierData>();
        float multiplier = 1f + data.upgradeValuePerLevel * (Level - 1);

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
