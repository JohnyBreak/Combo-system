using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    [Header("Rootmotion settings")]
    public AnimationCurve ForwardMovementCurve;
    public float ForwardMovementDistance = 1f;
    public float ForwardMovementDuration = 0.3f;
    public Vector3 ForwardMovementDirection = Vector3.forward;
    private Vector3 startPos;
    private Vector3 endPos;

    private float _elapsedTime = 0;

    void Start()
    {
        //StartCoroutine(Motion());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            StopAllCoroutines();
            //transform.position = startPos.position;
            StartCoroutine(Motion());
        }
    }

    private IEnumerator Motion() 
    {
        startPos = transform.position;

        endPos = transform.position + (transform.forward * ForwardMovementDirection.z + transform.right * ForwardMovementDirection.x + transform.up * ForwardMovementDirection.y);
        //endPos = new Vector3(transform.position.x + ForwardMovementDirection.x, transform.position.y + ForwardMovementDirection.y, transform.position.z + ForwardMovementDirection.z);
        Debug.Log(endPos);
        _elapsedTime = 0;
        Debug.Log(Time.time);
        while (_elapsedTime < ForwardMovementDuration) 
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / ForwardMovementDuration;

            float curveValue = ForwardMovementCurve.Evaluate(t);
            
            transform.localPosition = Vector3.Lerp(startPos, endPos, curveValue);

            yield return null;
        }
        Debug.Log(Time.time);
    }
}
