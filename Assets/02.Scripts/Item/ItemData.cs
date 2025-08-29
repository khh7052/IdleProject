using UnityEngine;
using Constants;

[CreateAssetMenu(menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;
    
    public StatModifierData[] baseModifiers; 

    public float upgradeValuePerLevel = 0.05f; 
    public int maxLevel = 100;
    public int level = 0;

    public float baseUpgradeCost = 10;
    public float costMultiplier = 1.2f;

    public float GetUpgradeCost()
    {
        level = Mathf.Clamp(level, 1, maxLevel);
        return baseUpgradeCost * Mathf.Pow(costMultiplier, level - 1);
    }
}
