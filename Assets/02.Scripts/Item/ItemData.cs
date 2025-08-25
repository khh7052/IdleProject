using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    
    public StatModifierData[] baseModifiers; 

    public float upgradeValuePerLevel = 0.05f; 
    public int maxLevel = 100;

    public float baseUpgradeCost = 10;
    public float costMultiplier = 1.2f;

    public float GetUpgradeCost(int level)
    {
        level = Mathf.Clamp(level, 1, maxLevel);
        return baseUpgradeCost * Mathf.Pow(costMultiplier, level - 1);
    }
}
