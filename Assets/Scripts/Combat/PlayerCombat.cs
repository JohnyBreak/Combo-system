using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private List<AttackSO> _attacks;
    [SerializeField] private PlayerAnimations _playerAnimations;
    private int _comboCounter;
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

        _attackInProcess = true;
        _shouldContinueCombo = false;
    }

    private void ContinueCombo()
    {
        if (!_attackInProcess) return;
        if ((_playerAnimations.GetCurrentStateNormalizedTime() >= _attacks[_comboCounter].NormalizedCrossFadeStartTime
            && _playerAnimations.GetCurrentStateNormalizedTime() < _attacks[_comboCounter].NaturalExitTime)
            && _playerAnimations.InAttackState()
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

    private void ExitAttack()
    {
        if (!_attackInProcess) return;

        if (_playerAnimations.GetCurrentStateNormalizedTime() > _attacks[_comboCounter].NaturalExitTime
            && _playerAnimations.InAttackState())
        {
            Debug.Log("NaturalExitTime");
            _playerAnimations.CrossfadeToMovementBlendTree();
            _comboCounter = 0;
            _attackInProcess = false;
            GetComponent<PlayerMovement>().ToggleMovementOnAttackAnimation(true);
            _shouldContinueCombo = false;
            return;
        }

        if (_shouldContinueCombo) return;
        if (_playerAnimations.IsNextStateIsAttack()) return;


        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (_playerAnimations.GetCurrentStateNormalizedTime() > _attacks[_comboCounter].InputExitTime
            && _playerAnimations.InAttackState()
            && input != Vector3.zero
            && !_playerAnimations.IsInTransition())
        {
            _playerAnimations.CrossfadeToMovementBlendTree();
            _comboCounter = 0;
            _attackInProcess = false;

            GetComponent<PlayerMovement>().ToggleMovementOnAttackAnimation(true);
            _shouldContinueCombo = false;
            return;
        }

        

        

        //if (_playerAnimations.GetCurrentStateNormalizedTime() > _attacks[_comboCounter].NaturalExitTime
        //    && _playerAnimations.InAttackState()
        //    && !_playerAnimations.IsInTransition()
        //    )
        //{
        //    _playerAnimations.CrossfadeToMovementBlendTree();
        //    //_animator.StopPlayback();
        //    //_animator.CrossFade("Blend Tree", 0.1f);
        //    _comboCounter = 0;
        //    _attackInProcess = false;
        //    _shouldContinueCombo = false;
        //}
    }
}
