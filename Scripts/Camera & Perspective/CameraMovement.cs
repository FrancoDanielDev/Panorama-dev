using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _movementVelocity;
    [SerializeField] private AnimationCurve _moveAnimation;
    [SerializeField, Range(0, 0.5f)] private float _edgeOffset;

    private bool _onMoving = false;

    private void LateUpdate()
    {
        var viewportPosition = Camera.main.WorldToViewportPoint(Player.instance.transform.position);

        if (viewportPosition.x + _edgeOffset >= 1 && !_onMoving)
        {
            StartCoroutine(Move(_movementVelocity));
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        _onMoving = true;
        MenuManager.instance.InteractionsInCooldown(true);
        var initialPosition = transform.position;
        var animationDuration = _moveAnimation.keys[_moveAnimation.keys.Length - 1].time;

        for (float time = 0; time < animationDuration; time += Time.deltaTime)
        {
            var movement = Vector3.Lerp(initialPosition, initialPosition + direction, _moveAnimation.Evaluate(time)) - transform.position;
            transform.Translate(movement);
            yield return null;
        }
        MenuManager.instance.InteractionsInCooldown(false);
        transform.position = initialPosition + direction;
        _onMoving = false;
    }
}
