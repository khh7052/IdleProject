using System;
using Constants;

[Serializable]
public struct StatModifierData
{
    public StatType statType;
    public ModifierType modifierType;
    public float value;

    public bool useDuration;   // 지속 시간 사용 여부
    public float duration;     // 지속 시간 (초)
    public object source;      // 아이템, 버프 등 출처

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
