using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<AttackSO> _attacks;

    //private float _lastTimeAttackClicked;
    //private float _lastComboEnd;
    private int _comboCounter;
    private int _attackHash = Animator.StringToHash("Attack");
    private bool _shouldContinueCombo = false;
    private bool _attackInProcess = false;
    //private bool _canExit = true;
    //private bool _canContinueCombo = false;

    //private void Start()
    //{
    //    Debug.Log(_attacks[0].Clip.name);
    //}

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    _animator.StopPlayback();
        //    _animator.CrossFade(_attacks[0].Clip.name, 0.25f);
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    _animator.StopPlayback();
        //    _animator.CrossFade(_attacks[1].Clip.name, 0.25f);
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    _animator.StopPlayback();
        //    _animator.CrossFade(_attacks[2].Clip.name, 0.25f);
        //}



        //if (_animator.IsInTransition(0)) return;

        ContinueCombo();
        //CheckNewState();

        ExitAttack();
        if (_shouldContinueCombo) return;

        //Debug.Log(_attacks.Count);

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

            //if (_attackInProcess && _shouldContinueCombo)
            //{
            //    Attack();
            //}
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

    //private void CheckNewState()
    //{
    //    if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2f
    //       && _animator.GetCurrentAnimatorStateInfo(0).tagHash == _attackHash
    //       && !_animator.IsInTransition(0)
    //       && _shouldContinueCombo)
    //    {
    //        _shouldContinueCombo = false;
    //    }
    //}

    private void ContinueCombo()
    {
        if (!_attackInProcess) return;
        if ((_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > _attacks[_comboCounter].NormalizedCrossFadeStartTime
            && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.94f)
            && _animator.GetCurrentAnimatorStateInfo(0).tagHash == _attackHash 
            && !_animator.IsInTransition(0))
        {
            //Debug.Log("CanContinueCombo");
            if (_shouldContinueCombo)
            {
                // _shouldContinueCombo = false;
                _comboCounter++;
                if (_comboCounter >= _attacks.Count)
                {
                    _comboCounter = 0;
                    return;
                }
                //Debug.Log("ShouldContinueCombo");
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
            //Debug.Log("ExitAttack");
            //Invoke(nameof(EndCombo), 0.1f);

            _animator.StopPlayback();
            _animator.CrossFade("Idle", 0.1f);
            _comboCounter = 0;
            _attackInProcess = false;
            _shouldContinueCombo = false;
        }
    }

    private void StartMove(AnimationCurve curve, Vector3 direction, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(Motion(curve, direction, duration));
    }

    private IEnumerator Motion(AnimationCurve curve, Vector3 direction, float duration)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + (transform.forward * direction.z + transform.right * direction.x + transform.up * direction.y);

        float _elapsedTime = 0;

        while (_elapsedTime < duration)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;

            float curveValue = curve.Evaluate(t);

            transform.localPosition = Vector3.Lerp(startPos, endPos, curveValue);

            yield return null;
        }
    }


    /// <summary>
    /// инкрементить каунтер только если отыграла анимация, нельзя перескакивать анимации, если у нас анимация играется 4, то нельзя инкрементить на 6
    /// </summary>


    //private void EndCombo()
    //{
    //    //если мы закончили комбо или прервали его, сделать кроссфейд в айдл
    //    Debug.Log("EndCombo");
    //    _comboCounter = 0;
    //    _attackInProcess = false;
    //    _shouldContinueCombo = false;
    //    _canExit = true;
    //    //_lastComboEnd = Time.time;
    //}
}
