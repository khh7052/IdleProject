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

    // 업그레이드 시도
    public bool TryUpgrade(ref float gold)
    {
        float cost = data.GetUpgradeCost(level);
        if (gold >= cost && level < data.maxLevel)
        {
            gold -= cost;
            level++;

            // 스탯 적용
            CharacterStats stats = GameManager.Instance.player.Stats;
            stats.Unequip(this);
            stats.Equip(this);

            Debug.Log($"Upgraded {Name} to Level {level}");
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
