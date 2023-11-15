using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Attacks/Normal Attack")]
public class AttackSO : ScriptableObject
{
    //public AnimatorOverrideController AnimatorOV;
    public AnimationClip Clip;
    public int Damage;

    [Header("Transition settings")]
    
    [Range(0,1)] public float NormalizedCrossFadeStartTime = 0.5f;
    [Range(0, 1)] public float NormalizedCrossFadeDuration = 0.2f;

    [Tooltip("Normalized start time")]
    [Range(0, 1)] public float NormalizedTimeOffset = 0f;

    [Tooltip("Normalized start time")]
    [Min(0.1f)] public float AnimSpeedMultiplier = 1f;

    [Header("Attack break settings")]

    [Tooltip("Normalized start time")]
    [Range(0, 1)] public float NaturalExitTime = .9f;
    [Range(0, 1)] public float InputExitTime = .7f;
}