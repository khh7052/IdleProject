using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text costText;

    public Item Item => item;

    public void UpgradeItem()
    {
        float gold = GameManager.Instance.Gold;
        if (item.TryUpgrade(ref gold))
        {
            GameManager.Instance.SetGold((ulong)gold);
            RefreshUI();
        }
    }

    public void RefreshUI()
    {
        iconImage.sprite = item.data.icon;
        nameText.text = $"{item.Name} Lv.{item.Level}";
        costText.text = $"Cost: {item.data.GetUpgradeCost(item.Level):F1}G";
        button.interactable = item.Level < item.data.maxLevel;
    }

}
