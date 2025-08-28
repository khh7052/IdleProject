using System;
using System.Collections.Generic;
using UnityEngine;
using Constants;

[Serializable]
public class Stat
{
    public event Action IsDirtyChanged;
    public event Action<float> FinalValueChanged;

    [SerializeField] private StatType type;
    [SerializeField] private float baseValue;

    // ���ο����� ���̴� Ÿ�̸� ��� ������̾�
    protected class TimedModifier
    {
        public StatModifierData Modifier { get; }
        private readonly float expireTime; // 0�̸� ������

        public TimedModifier(StatModifierData modifier)
        {
            Modifier = modifier;
            expireTime = modifier.useDuration ? Time.time + modifier.duration : 0f;
        }

        public bool IsExpired => Modifier.useDuration && Time.time >= expireTime;
    }

    private List<TimedModifier> modifiers = new();
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

    public virtual void Initialize()
    {
        modifiers ??= new();
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

    // ----------------- ���� ���� -----------------
    private void RemoveExpiredModifiers()
    {
        if (modifiers.RemoveAll(m => m == null || m.IsExpired) > 0)
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

    private void MarkDirty()
    {
        isDirty = true;
        IsDirtyChanged?.Invoke();
    }
}
