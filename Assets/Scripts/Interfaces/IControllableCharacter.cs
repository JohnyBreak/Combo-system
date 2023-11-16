using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllableCharacter
{
    public void Move(Vector3 direction);
    public void TryPerformLightAttack();
}
