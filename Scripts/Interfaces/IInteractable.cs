using UnityEngine;

public interface IInteractable
{
    void Focus();

    void Unfocus();

    void Interact(GameObject interactor);
}
