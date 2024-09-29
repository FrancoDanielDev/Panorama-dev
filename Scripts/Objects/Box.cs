using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] private AnimationCurve _moveAnimation;
    [SerializeField] private Collider _trigger;
    [Space]
    [SerializeField] private float _boxForce;

    private Renderer _renderer;
    private bool _onEnabled = true;
    public bool canInteract = true;

    public bool OnEnabled
    {
        get => _onEnabled;

        set
        {
            _onEnabled = value;
        }
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Focus()
    {
        _renderer.material.color = Color.red;
    }

    public void Unfocus()
    {
        _renderer.material.color = Color.white;
    }

    public void Interact(GameObject interactor)
    {
        if (!_onEnabled || !canInteract) return;
        var direction = interactor.transform.position - transform.position;
        var x = Vector3.Dot(direction, transform.right);
        var z = Vector3.Dot(direction, transform.forward);

        var isXBigger = Mathf.Abs(x) > Mathf.Abs(z);
        var movementDirection = isXBigger ? Vector3.right * Mathf.Sign(x) : Vector3.forward * Mathf.Sign(z);

        movementDirection *= _boxForce;

        StartCoroutine(Move(-movementDirection));

        AudioManager.instance.Play("MoveBox");
    }

    private IEnumerator Move(Vector3 direction)
    {
        OnEnabled = false;
        var initialPosition = transform.position;
        var animationDuration = _moveAnimation.keys[_moveAnimation.keys.Length - 1].time;

        for (float time = 0; time < animationDuration; time += Time.deltaTime)
        {
            var movement = Vector3.Lerp(initialPosition, initialPosition + direction, _moveAnimation.Evaluate(time)) - transform.position;
            transform.Translate(movement);
            yield return null;
        }

        OnEnabled = true;
    }
}
