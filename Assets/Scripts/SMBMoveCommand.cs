using UnityEngine;

public class SMBMoveCommand : StateMachineBehaviour
{
    [Header("Rootmotion settings")]
    [SerializeField] private AnimationCurve _movementCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField] private float _movementDuration = 0.3f;
    [SerializeField] private Vector3 _movementDirection = Vector3.forward;
    private Vector3 startPos;
    private Vector3 endPos;
    private Transform _player;
    private Transform _graphicParent;
    private float _elapsedTime = 0;
    private bool _move = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _graphicParent = animator.transform.parent;
        _player = animator.transform.root;

        StartMove();
    }

    private void StartMove()
    {
        startPos = _player.position;

        endPos = _player.position + (_player.forward * _movementDirection.z + _player.right * _movementDirection.x + _player.up * _movementDirection.y);

        _elapsedTime = 0;
        _move = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _move = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_move) return;

        _elapsedTime += Time.deltaTime;
        float t = _elapsedTime / _movementDuration;

        float curveValue = _movementCurve.Evaluate(t);

        _player.localPosition = Vector3.Lerp(startPos, endPos, curveValue);
        animator.transform.position = _graphicParent.position;
        if (curveValue == 1) _move = false;
    }
}
