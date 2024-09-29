using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int _platesRequired;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _collider;
    [Space]
    [SerializeField] private Box[] _boxes;

    private int _counter;

    public void ObjectOnTop()
    {
        _counter++;

        if (_counter >= _platesRequired)
        {
            ActivateDoor();
        }
    }

    public void ObjectOutOfPlace()
    {
        _counter--;
    }

    private void ActivateDoor()
    {
        if (_anim != null) _anim.SetTrigger("Open");

        foreach (Box item in _boxes)
        {
            item.canInteract = false;
        }

        _collider.SetActive(false);
        AudioManager.instance.Play("OpenDoor");
    }
}
