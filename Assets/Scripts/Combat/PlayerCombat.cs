using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<AttackSO> _attacks;

    private int _comboCounter;
    private int _attackHash = Animator.StringToHash("Attack");
    private bool _shouldContinueCombo = false;
    private bool _attackInProcess = false;

    private void Update()
    {
        ContinueCombo();

        ExitAttack();
        if (_shouldContinueCombo) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!_attackInProcess)
            {
                Attack();
                return;
            }

            if (_attackInProcess && _comboCounter < _attacks.Count - 1)
            {
                _shouldContinueCombo = true;
                return;
            }
        }

    }

    private void Attack()
    {
        _animator.StopPlayback();
        _animator.CrossFade(_attacks[_comboCounter].Clip.name, 
            _attacks[_comboCounter].NormalizedCrossFadeDuration,
            0, 
            _attacks[_comboCounter].NormalizedTimeOffset);

        _attackInProcess = true;
        _shouldContinueCombo = false;
    }

    private void ContinueCombo()
    {
        if (!_attackInProcess) return;
        if ((_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > _attacks[_comboCounter].NormalizedCrossFadeStartTime
            && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.94f)
            && _animator.GetCurrentAnimatorStateInfo(0).tagHash == _attackHash 
            && !_animator.IsInTransition(0))
        {
            if (_shouldContinueCombo)
            {
                _comboCounter++;
                if (_comboCounter >= _attacks.Count)
                {
                    _comboCounter = 0;
                    return;
                }
                Attack();
            }
        }
    }

    private void ExitAttack()
    {
        if (!_attackInProcess) return;
        if (_shouldContinueCombo) return;

        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.94f
            && _animator.GetCurrentAnimatorStateInfo(0).tagHash == _attackHash
            && !_animator.IsInTransition(0))
        {
            _animator.StopPlayback();
            _animator.CrossFade("Blend Tree", 0.1f);
            _comboCounter = 0;
            _attackInProcess = false;
            _shouldContinueCombo = false;
        }
    }
}
