using GD.MinMaxSlider;
using UnityEngine;

public class SMBBlockMoveInput : StateMachineBehaviour
{
    [MinMaxSlider(0, 1)] public Vector2 NormalizedMoveBlockTime;

    private int _attackHash = Animator.StringToHash("Attack");
    //private PlayerMovement _playerMovement;
    private bool _shouldBlockMove = true;
    private bool _shouldLetMove = true;
    private bool _skip = false;

    //public void SetPlayerMovement(PlayerMovement playerMovement)
    //{
    //    _playerMovement = playerMovement;
    //}

    private PlayerInput _playerInput;
    public void SetPlayerInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerInput.ToggleMovement(false);
        //_playerMovement.ToggleMovementOnAttackAnimation(false);
        _shouldBlockMove = true;
        _shouldLetMove = true;
        _skip = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(0)
                    && animator.GetNextAnimatorStateInfo(0).tagHash == _attackHash)
        {
            _skip = true;
            return;
        }
        if (stateInfo.normalizedTime >= NormalizedMoveBlockTime.x
            && stateInfo.normalizedTime <= NormalizedMoveBlockTime.y
            && _shouldBlockMove)
        {
            _shouldBlockMove = false;
            _shouldLetMove = true;
            _playerInput.ToggleMovement(false);
            //_playerMovement.ToggleMovementOnAttackAnimation(false);
            return;
        }

        if ((stateInfo.normalizedTime <= NormalizedMoveBlockTime.x
            || stateInfo.normalizedTime > NormalizedMoveBlockTime.y)
            && _shouldLetMove)
        {
            _shouldLetMove = false;
            _shouldBlockMove = true;
            _playerInput.ToggleMovement(true);
            //_playerMovement.ToggleMovementOnAttackAnimation(true);
            return;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_skip) return;
        _playerInput.ToggleMovement(true);
        //_playerMovement.ToggleMovementOnAttackAnimation(true);
        _shouldBlockMove = true;
        _shouldLetMove = true;
    }

}
