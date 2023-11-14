using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        if (direction != Vector3.zero) 
        {
            transform.position += direction.normalized * Time.deltaTime * _speed;
        }
    }
}
