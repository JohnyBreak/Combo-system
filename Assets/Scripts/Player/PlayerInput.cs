using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private IControllableCharacter _controllable;
    // Start is called before the first frame update
    void Awake()
    {
        _controllable = GetComponent<IControllableCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadMoveInput();
        ReadLightAttack();
    }

    private void ReadMoveInput() 
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _controllable.Move(direction);
    }

    private void ReadLightAttack() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            _controllable.TryPerformLightAttack();
        }
    }
}
