using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 360f;

    private bool _canTurn = true;

    //private void Awake()
    //{
    //    var behs = GetComponentInChildren<Animator>().GetBehaviours<SMBEnableTurn>();

    //    foreach (var beh in behs)
    //    {
    //        beh.SetPlayerRotation(this);
    //    }
    //}

    public void ToggleTurnOnAttackAnimation(bool canTurn)
    {
        _canTurn = canTurn;
    }

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        if (!_canTurn) return;

        if (direction != Vector3.zero)
        {
            //transform.forward =  direction.normalized;

            Quaternion toRotate = Quaternion.LookRotation(direction.normalized, transform.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, _rotationSpeed * Time.deltaTime);
        }
    }
}
