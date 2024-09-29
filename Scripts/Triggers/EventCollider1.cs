using System.Collections;
using UnityEngine;

public class EventCollider1 : MonoBehaviour
{
    [SerializeField] private GameObject _textUi;
    [SerializeField] private float _time;

    private Coroutine _detected = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if (_detected == null)
                _detected = StartCoroutine(TextActive());
        }
    }

    private IEnumerator TextActive()
    {
        _textUi.SetActive(true);

        yield return new WaitForSeconds(_time);

        _textUi.SetActive(false);

        gameObject.SetActive(false);
    }
}
