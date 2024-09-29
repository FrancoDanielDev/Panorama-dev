using UnityEngine;

public class EventCollider2 : MonoBehaviour
{
    [SerializeField] private GameObject _endWindow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _endWindow.SetActive(true);
        }
    }
}
