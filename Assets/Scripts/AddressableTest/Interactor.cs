using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Chest _interactable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            _interactable.Interact();
        }
    }
}
