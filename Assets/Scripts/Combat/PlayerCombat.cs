using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //[SerializeField] private Animator _animator;
    [SerializeField] private List<AttackSO> _attacks;
    [SerializeField] private PlayerAnimations _playerAnimations;
    private int _comboCounter;
    //private int _attackHash = Animator.StringToHash("Attack");
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
        _playerAnimations.CrossfadeAttackAnimation(_attacks[_comboCounter].Clip.name,
            _attacks[_comboCounter].NormalizedCrossFadeDuration,
            _attacks[_comboCounter].NormalizedTimeOffset, _attacks[_comboCounter].AnimSpeedMultiplier);

        //_animator.StopPlayback();

        //_animator.CrossFade(_attacks[_comboCounter].Clip.name, 
        //    _attacks[_comboCounter].NormalizedCrossFadeDuration,
        //    0, 
        //    _attacks[_comboCounter].NormalizedTimeOffset);

        //_playerAnimations.SetAttackSpeedMultiplier(_attacks[_comboCounter].AnimSpeedMultiplier);

        _attackInProcess = true;
        _shouldContinueCombo = false;
    }

    private void ContinueCombo()
    {
        if (!_attackInProcess) return;
        if ((_playerAnimations.GetCurrentStateNormalizedTime() > _attacks[_comboCounter].NormalizedCrossFadeStartTime
            && _playerAnimations.GetCurrentStateNormalizedTime() < 0.94f)
            && _playerAnimations.IsAttackState()
            && !_playerAnimations.IsInTransition())
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
    // найти анин старый телефон
    private void ExitAttack()
    {
        if (!_attackInProcess) return;
        if (_shouldContinueCombo) return;

        if (_playerAnimations.GetCurrentStateNormalizedTime() > 0.94f
            && _playerAnimations.IsAttackState()
            && !_playerAnimations.IsInTransition())
        {
            _playerAnimations.CrossfadeToMovementBlendTree();
            //_animator.StopPlayback();
            //_animator.CrossFade("Blend Tree", 0.1f);
            _comboCounter = 0;
            _attackInProcess = false;
            _shouldContinueCombo = false;
        }
    }
}
