using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _smoothBlend = 0.1f;
    private int _movementSpeed = Animator.StringToHash("MovementSpeed");
   
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetMovementAnimation(float currentMovement)
    {
        _animator.SetFloat(_movementSpeed, currentMovement, _smoothBlend, Time.deltaTime);
    }
}
