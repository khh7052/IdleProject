using System;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[Serializable]
public class Stat
{
    public event Action<float> FinalValueChanged;

    [SerializeField] private StatType type;
    [SerializeField] private float baseValue;

    // 내부에서만 쓰이는 타이머 기반 모디파이어
    protected class TimedModifier
    {
        public StatModifierData Modifier { get; }
        private readonly float expireTime; // 0이면 무제한

        public TimedModifier(StatModifierData modifier)
        {
            Modifier = modifier;
            expireTime = modifier.useDuration ? Time.time + modifier.duration : 0f;
        }

        public bool IsExpired => Modifier.useDuration && Time.time >= expireTime;
    }

    private readonly List<TimedModifier> modifiers = new();
    private float finalValue;
    private bool isDirty = true;

    public StatType Type => type;
    public float BaseValue => baseValue;
    public float FinalValue
    {
        get
        {
            RemoveExpiredModifiers();
            if (isDirty) RecalculateFinalValue();
            return finalValue;
        }
    }

    public Stat(StatType type, float baseValue = 0f)
    {
        this.type = type;
        this.baseValue = baseValue;
        finalValue = baseValue;
    }

    public void SetBaseValue(float value)
    {
        if (Mathf.Approximately(baseValue, value)) return;
        baseValue = value;
        MarkDirty();
    }

    public void AddModifier(StatModifierData modifier)
    {
        modifiers.Add(new TimedModifier(modifier));
        MarkDirty();
    }

    public void RemoveModifiersBySource(object source)
    {
        if (modifiers.RemoveAll(m => m.Modifier.source == source) > 0)
            MarkDirty();
    }

    // ----------------- 내부 로직 -----------------
    private void RemoveExpiredModifiers()
    {
        if (modifiers.RemoveAll(m => m.IsExpired) > 0)
            MarkDirty();
    }

    private void RecalculateFinalValue()
    {
        float addSum = 0f;
        float mulSum = 1f;

        foreach (var mod in modifiers)
        {
            switch (mod.Modifier.modifierType)
            {
                case ModifierType.Additive:
                    addSum += mod.Modifier.value;
                    break;
                case ModifierType.Multiplicative:
                    mulSum += mod.Modifier.value;
                    break;
            }
        }

        finalValue = (baseValue + addSum) * mulSum;
        isDirty = false;
        FinalValueChanged?.Invoke(finalValue);
    }

    private void MarkDirty() => isDirty = true;
}
