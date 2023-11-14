using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using GameOn.TagMaskField;
public class ViewArc : MonoBehaviour
{
    [SerializeField] TagMask _tagMask = TagMask.Nothing;

    [Tooltip("Transform for projecting the viewing arc")]
    [SerializeField] private Transform _arcTransform;

    [Tooltip("Viewing arc angle")]
    [SerializeField]
    private float _angle = 45f;

    [Tooltip("Viewing arc radius")]
    [SerializeField]
    private float _radius = 45f;

    [Tooltip("Viewing arc color")]
    [SerializeField]
    private Color _color = Color.red;

    private void OnDrawGizmosSelected()
    {
        if (_arcTransform != null) 
        {
            Vector3 from = Quaternion.AngleAxis(-_angle, _arcTransform.up) * _arcTransform.forward;

            Handles.color = _color;
            Handles.DrawSolidArc(_arcTransform.position, _arcTransform.up, from, _angle * 2f, _radius);
        }
    }
}
