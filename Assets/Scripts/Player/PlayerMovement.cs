using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimations _anim;
    [SerializeField] private float _speed = 5;

    private bool _canMove = true;

    private void Awake()
    {
        var behs = GetComponentInChildren<Animator>().GetBehaviours<SMBBlockMoveInput>();

        foreach (var beh in behs)
        {
            beh.SetPlayerMovement(this);
        }
    }

    public void ToggleMovementOnAttackAnimation(bool canMove)
    {
        _canMove = canMove;
    }

    private void Update()
    {
        if (!_canMove) return;

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (direction != Vector3.zero)
        {
            transform.position += direction.normalized * Time.deltaTime * _speed;

            _anim.SetMovementAnimation(1);
            return;
        }
        _anim.SetMovementAnimation(0);
    }
}
