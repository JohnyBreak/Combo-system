using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IControllableCharacter
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerCombat _combat;
    
    private Vector3 _movedirection;

    private void Awake()
    {
        _combat.SetInput(GetComponent<PlayerInput>());
        var behs = GetComponentInChildren<Animator>().GetBehaviours<SMBEnableTurn>();

        foreach (var beh in behs)
        {
            beh.SetPlayerInput(_input);
        }
        var behs2 = GetComponentInChildren<Animator>().GetBehaviours<SMBBlockMoveInput>();

        foreach (var beh in behs2)
        {
            beh.SetPlayerInput(_input);
        }
        _movement.SetInput(_input);
    }

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
