using UnityEngine;

public class ChangePerspective : MonoBehaviour
{
    [SerializeField] private Perspective _newPerspective;
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.Perspective = _newPerspective;
        gameObject.SetActive(false);
    }
}
