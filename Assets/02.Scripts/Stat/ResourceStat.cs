using UnityEngine;
using Constants;
using System;

[System.Serializable]
public class ResourceStat : Stat
{
    public event Action<float> OnCurrentValueChanged;

    [SerializeField] private float currentValue;

    public ResourceStat(StatType type, float baseValue) : base(type, baseValue)
    {
        SetCurrentValue(FinalValue);
        FinalValueChanged += SetCurrentValue;
    }

    public float CurrentValue => currentValue;

    public void SetCurrentValue(float value)
    {
        float clampedValue = Mathf.Clamp(value, 0, FinalValue);
        if (currentValue == clampedValue) return;
        currentValue = clampedValue;
        OnCurrentValueChanged?.Invoke(currentValue);
    }

    public void Restore(float amount) => SetCurrentValue(CurrentValue + amount);
    public void Reduce(float amount) => SetCurrentValue(CurrentValue - amount);

}
