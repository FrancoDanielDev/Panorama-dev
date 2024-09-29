using System.Collections;
using UnityEngine;

public class ChangeCameraPerspective : MonoBehaviour
{
    public static ChangeCameraPerspective Instance;

    [SerializeField] private Transform _pivotPoint;
    [SerializeField] private AnimationCurve _animation;

    public bool _canChange;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        EventManager.Subscribe(PerspectiveHasChanged, Events.ChangePerspective);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (GameManager.Instance.onTransition || !_canChange) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.Perspective = Perspective.Side;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.Perspective = Perspective.Top;
        }
    }

    public void PerspectiveHasChanged()
    {
        if (GameManager.Instance.Perspective == Perspective.Side)
        {
            StartCoroutine(Transition(0));
        }
        else
        {
            StartCoroutine(Transition(90));
        }
    }

    private IEnumerator Transition(float finalAngle)
    {
        GameManager.Instance.onTransition = true;
        Time.timeScale = 0;

        var initialAngle = Camera.main.transform.rotation.eulerAngles.x;
        var animationDuration = _animation.keys[_animation.keys.Length - 1].time;

        for (float time = 0; time < animationDuration; time += Time.unscaledDeltaTime)
        {
            var rotation = Mathf.Lerp(initialAngle, finalAngle, _animation.Evaluate(time)) - Camera.main.transform.rotation.eulerAngles.x;
            Camera.main.transform.RotateAround(_pivotPoint ? _pivotPoint.position : Vector3.zero, Vector3.right, rotation);
            yield return null;
        }

        Camera.main.transform.rotation = Quaternion.Euler(Vector3.right * finalAngle);
        GameManager.Instance.onTransition = false;
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(PerspectiveHasChanged, Events.ChangePerspective);
    }
}
