using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _smoothBlend = 0.1f;
    
    private int _movementBT = Animator.StringToHash("Blend Tree");
    private int _attackHash = Animator.StringToHash("Attack");
    private int _movementSpeed = Animator.StringToHash("MovementSpeed");
    private int _attackSpeedMultiplier = Animator.StringToHash("AttackSpeedMultiplier");

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetMovementAnimation(float currentMovement)
    {
        _animator.SetFloat(_movementSpeed, currentMovement, _smoothBlend, Time.deltaTime);
    }
    public void SetMovementAnimationRaw(float currentMovement)
    {
        _animator.SetFloat(_movementSpeed, currentMovement);
    }


    public void SetAttackSpeedMultiplier(float speed = 1f) 
    {
        if (speed <= 0) speed = 0.1f;
        _animator.SetFloat(_attackSpeedMultiplier, speed);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attackSpeedMultiplier">Min value 0.1f</param>
    public void CrossfadeAttackAnimation(string attackClipName, float normalizedCrossFadeDuration, float normalizedTimeOffset, float attackSpeedMultiplier = 1f)
    {
        _animator.StopPlayback();

        _animator.CrossFade(attackClipName,
            normalizedCrossFadeDuration,
            0,
            normalizedTimeOffset);

        if (attackSpeedMultiplier <= 0) attackSpeedMultiplier = 0.1f;
        _animator.SetFloat(_attackSpeedMultiplier, attackSpeedMultiplier);
    }

    public float GetCurrentStateNormalizedTime() 
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public AnimatorStateInfo GetNextAnimatorStateInfo()
    {
        return _animator.GetNextAnimatorStateInfo(0);
    }

    public AnimatorStateInfo GetCurrentAnimatorStateInfo()
    {
        return _animator.GetCurrentAnimatorStateInfo(0);
    }

    public bool InAttackState()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).tagHash == _attackHash;
    }

    public bool IsNextStateIsAttack()
    {
        return _animator.GetNextAnimatorStateInfo(0).tagHash == _attackHash;
    }

    public bool IsInTransition()
    {
        return _animator.IsInTransition(0);
    }

    public void CrossfadeToMovementBlendTree(float normalizedCrossFadeDuration = 0.1f) 
    {
        _animator.StopPlayback();
        _animator.CrossFade(_movementBT, normalizedCrossFadeDuration);
    }
}
