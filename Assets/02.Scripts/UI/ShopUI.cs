using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private ItemButton[] itemButtons;

    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        for (int i = 0; i < itemButtons.Length; i++)
            itemButtons[i].RefreshUI();
    }
}
