//using GD.MinMaxSlider;
using UnityEngine;

public class SMBEnableTurn : StateMachineBehaviour
{
    //[Tooltip("Normalized time when player can turn")]
    //[MinMaxSlider(0, 1)] public Vector2 TurnTime;

    private int _attackHash = Animator.StringToHash("Attack");
    private PlayerRotation _playerRotation;
    //private bool _shouldBlockTurn = true;
    //private bool _shouldLetTurn = true;
    private bool _skip = false;

    public void SetPlayerRotation(PlayerRotation playerRotation) 
    {
        _playerRotation = playerRotation;
        
    }
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _skip = false;
        _playerRotation.ToggleTurnOnAttackAnimation(false);
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (direction != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(direction.normalized, animator.transform.root.up);

            animator.transform.root.rotation = toRotate;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(0)
                    && animator.GetNextAnimatorStateInfo(0).tagHash == _attackHash
                    && stateInfo.normalizedTime > 0.5f)
        {
            _skip = true;
            return;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_skip) return;

        _playerRotation.ToggleTurnOnAttackAnimation(true);
        //_shouldBlockTurn = true;
        //_shouldLetTurn = true;
    }
}
