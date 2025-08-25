using System;
using Constants;

[Serializable]
public struct StatModifierData
{
    public StatType statType;
    public ModifierType modifierType;
    public float value;

    public bool useDuration;   // ���� �ð� ��� ����
    public float duration;     // ���� �ð� (��)
    public object source;      // ������, ���� �� ��ó

    public StatModifierData(StatType type, ModifierType modType, float val, object src = null, bool useDur = false, float dur = 0f)
    {
        statType = type;
        modifierType = modType;
        value = val;
        source = src;
        useDuration = useDur;
        duration = dur;
    }
}
