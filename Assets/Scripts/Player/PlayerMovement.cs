using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerAnimations _anim;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _rotationSpeed = 1080f;
    private bool _canMove = true;
    //private Vector3 _direction;
    private PlayerInput _input;
    
    public void SetInput(PlayerInput playerInput) 
    {
        _input = playerInput;
        _input.ToggleMovementEvent += ToggleMovementOnAttackAnimation;
    }

    //private void Awake()
    //{


    //    var behs = GetComponentInChildren<Animator>().GetBehaviours<SMBBlockMoveInput>();

    //    foreach (var beh in behs)
    //    {
    //        beh.SetPlayerMovement(this);
    //    }
    //}

    private void OnDestroy()
    {
        _input.ToggleMovementEvent -= ToggleMovementOnAttackAnimation;
    }

    public void ToggleMovementOnAttackAnimation(bool canMove)
    {
        _canMove = canMove;
        if(!_canMove) _anim.SetMovementAnimationRaw(0);
    }

    //private void Update()
    //{
    //    direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
    //    if (!_canMove) return;

    //    if (direction != Vector3.zero)
    //    {
    //        transform.position += direction.normalized * Time.deltaTime * _speed;

    //        _anim.SetMovementAnimation(1);
    //        return;
    //    }
    //    _anim.SetMovementAnimation(0);
    //}

    public void Move(Vector3 direction)
    {
        //_direction = direction;//new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (!_canMove) return;

        if (direction != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(direction.normalized, transform.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, _rotationSpeed * Time.deltaTime);

            transform.position += direction.normalized * Time.deltaTime * _speed;

            _anim.SetMovementAnimation(1);
            return;
        }
        _anim.SetMovementAnimation(0);
    }
}
