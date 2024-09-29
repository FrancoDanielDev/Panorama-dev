using UnityEngine;

public class EventCollider : MonoBehaviour
{
    [SerializeField] private GameObject _crystal;
    [SerializeField] private GameObject _NPC;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _crystal.SetActive(false);
            _NPC.SetActive(true);

            ChangeCameraPerspective.Instance._canChange = true;

            gameObject.SetActive(false);
        }
    }
}
