using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField] private LayerMask _objectMask;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("1");
        if (((1 << collision.gameObject.layer) & _objectMask) != 0)
        {
            Debug.Log("2");
            Destroy(collision.gameObject);
            return;
        }
    }
}
