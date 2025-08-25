using UnityEngine;
using Constants;

[CreateAssetMenu(menuName = "Data/StatModifierData")]
public class StatModifierData : ScriptableObject
{
    public StatType statType;
    public ModifierType modifierType;
    public float value;
    public bool useDuration = false; // ���� �ð� ��� ����
    public float duration = 0f; // ���� �ð� (�� ����)
    public object source; // ������, ���� �� ��ó
}
