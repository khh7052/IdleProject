using UnityEngine;
using Constants;
using System;

[Serializable]
public class ResourceStat : Stat
{
    public event Action<float> CurrentValueChanged;

    [SerializeField] private float currentValue;

    public override void Initialize()
    {
        base.Initialize();
        FinalValueChanged += OnFinalValueChanged;
    }

    public float CurrentValue => currentValue;


    public void OnFinalValueChanged(float value)
    {
        Debug.Log($"OnFinalValueChanged: {value} (FinalValue: {FinalValue})");
        if(currentValue > value)
        {
            currentValue = value;
            CurrentValueChanged?.Invoke(currentValue);
        }
    }

    public void SetCurrentValue(float value)
    {
        Debug.Log($"SetCurrentValue: {value} (FinalValue: {FinalValue})");
        float clampedValue = Mathf.Clamp(value, 0, FinalValue);
        if (currentValue == clampedValue) return;
        currentValue = clampedValue;
        CurrentValueChanged?.Invoke(currentValue);
    }

    public void Restore(float amount) => SetCurrentValue(CurrentValue + amount);
    public void Reduce(float amount) => SetCurrentValue(CurrentValue - amount);

}
