using UnityEngine;
namespace Constants
{
    public enum TeamType
    {
        Player,
        Enemy,
    }

    public enum StatType
    {
        HP,
        MP,
        Experience,
        Damage,
        SightRange,
        AttackRange,
        AttackRate,
        Defense,
        MoveSpeed,
    }

    public enum ModifierType
    {
        Additive,     // +5
        Multiplicative, // x1.2
    }

    public enum SoundType
    {
        BGM,
        SFX,
    }

    public enum VolumeType
    {
        Master,
        BGM,
        SFX,
    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Consumeable,
    }

    public static class AnimatorHash
    {
        public static int IdleHash = Animator.StringToHash("Idle");
        public static int ChaseHash = Animator.StringToHash("Chase");
        public static int AttackHash = Animator.StringToHash("Attack");
    }

}
