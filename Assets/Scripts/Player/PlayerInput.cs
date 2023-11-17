using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<bool> ToggleMovementEvent;
    private IControllableCharacter _controllable;
    private Vector3 _moveDirection;
    public Vector3 MoveDirection => _moveDirection;

    void Awake()
    {
        _controllable = GetComponent<IControllableCharacter>();
    }

    void Update()
    {
        ReadMoveInput();
        ReadLightAttack();
    }

    private void ReadMoveInput() 
    {
        _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


        _controllable.Move(_moveDirection);
    }

    public void ToggleMovement(bool canMove)
    {
        ToggleMovementEvent?.Invoke(canMove);
    }

    private void ReadLightAttack() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            _controllable.TryPerformLightAttack();
        }
    }
}
