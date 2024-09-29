using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Door[] _door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Box>())
        {
            for (int i = 0; i < _door.Length; i++)
            {
                _door[i].ObjectOnTop();
            }

            _anim.SetBool("Press", true);

            AudioManager.instance.Play("PressurePlate");
        }
    }

    private void OnTriggerExit(Collider other)
    {     
        if (other.GetComponent<Box>())
        {
            for (int i = 0; i < _door.Length; i++)
            {
                _door[i].ObjectOutOfPlace();
            }

            _anim.SetBool("Press", false);
        }
    }
}
