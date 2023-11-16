using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IControllableCharacter
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerCombat _combat;
    
    private Vector3 _movedirection;

    public void Move(Vector3 direction)
    {
        _movedirection = direction;
    }

    public void TryPerformLightAttack()
    {
        _combat.TryAttack();
    }

    private void Update()
    {
        _movement.Move(_movedirection);
    }
}
