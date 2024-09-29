using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IObserver
{
    private IInteractableObj _interactableObj;

    private void Start()
    {
        MenuManager.instance?.Subscribe(this);
    }

    public void Notify(string action)
    {
        if (action == "Interact" && _interactableObj != null)
            ExecuteInteraction();
    }

    private void ExecuteInteraction()
    {
        _interactableObj.Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<IInteractableObj>();

        if (obj != null)
        {
            _interactableObj = obj;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.GetComponent<IInteractableObj>();

        if (obj != null)
        {
            _interactableObj = null;
        }
    }
}
