using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AnimationMoveCommand : StateMachineBehaviour
{
    [Header("Rootmotion settings")]
    public AnimationCurve ForwardMovementCurve;
    public float ForwardMovementDuration = 0.3f;
    public Vector3 ForwardMovementDirection = Vector3.forward;
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

        endPos = _player.position + (_player.forward * ForwardMovementDirection.z + _player.right * ForwardMovementDirection.x + _player.up * ForwardMovementDirection.y);

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
        float t = _elapsedTime / ForwardMovementDuration;

        float curveValue = ForwardMovementCurve.Evaluate(t);

        _player.localPosition = Vector3.Lerp(startPos, endPos, curveValue);
        animator.transform.position = _graphicParent.position;
        if (curveValue == 1) _move = false;
    }
}
