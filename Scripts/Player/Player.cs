using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] private Rigidbody _body;

    [Header("Physics")]
    [SerializeField] private float _speed;
    [SerializeField] private float _fallMultiplier, _lowJumpMultiplier;
    [SerializeField] private float _jumpForce;

    [Header("Ground checker")]
    [SerializeField] private float _groundedRadius;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _checkUnderGroundDistance;
    [SerializeField] private float _raycastRadius;

    [SerializeField] private float _interactionDistance;

    [SerializeField] private GameObject _visualBody;
    public Animator animator;

    private SphereCollider _sphereCollider;
    private PlayerControllers _currentController;
    private Dictionary<Perspective, PlayerControllers> _controllers;

    #region Propiedades
    public float Speed => _speed;
    public Rigidbody Body => _body;
    public float FallMultiplier => _fallMultiplier;
    public float LowJumpMultiplier => _lowJumpMultiplier;
    public float JumpForce => _jumpForce;
    public LayerMask GroundMask => _groundMask;
    public Transform GroundPoint => _groundPoint;
    public float GroundDistance => _groundedRadius;
    public float UnderGroundDistance => _checkUnderGroundDistance;
    public bool isGrounded { get; private set; }
    #endregion

    private readonly HashSet<IInteractable> _interactables = new HashSet<IInteractable>();

    private void Awake()
    {
        if (instance == null) instance = this;

        _controllers = new()
        {
            { Perspective.Side, new HorizontalController(this) },
            { Perspective.Top, new VerticalController(this) },
        };

        _sphereCollider = GetComponent<SphereCollider>();

        EventManager.Subscribe(UpdateController, Events.ChangePerspective);
    }

    private void Start()
    {
        UpdateController();
    }

    private void Update()
    {
        _sphereCollider.radius = _interactionDistance;
        if (GameManager.Instance.onTransition) return;

        if (_currentController != null)
            _currentController.Update();


        if (Input.GetKeyDown(KeyCode.E) && isGrounded)
        {
            foreach (var item in _interactables)
            {
                item.Interact(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.OverlapSphere(_groundPoint.position, _groundedRadius, _groundMask).Length > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            var component = other.GetComponent<IInteractable>();
            component.Focus();
            _interactables.Add(component);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            var component = other.GetComponent<IInteractable>();
            component.Unfocus();
            _interactables.Remove(component);
        }
    }

    private void UpdateController()
    {
        _currentController = _controllers[GameManager.Instance.Perspective];
        _currentController.Start();
    }

    public void OnEnabled(bool value)
    {
        if (value)
            UpdateController();
        else
            _currentController = null;

        _body.useGravity = value;
    }

    /*public IEnumerator Climb(Transform startingPoint, Transform endPoint)
    {     
        while (Vector3.Distance(_visualBody.transform.position, endPoint.position) > 0.1f)
        {
            _visualBody.transform.position += Vector3.Lerp(startingPoint.position, endPoint.position, Time.deltaTime);
            Debug.Log("A");
            yield return null;
        }
    }*/

    /*public IEnumerator Climb(Transform startingPoint, Transform endPoint)
    {
        float time = 0;
        MenuManager.instance.InteractionsInCooldown(true);
        _visualBody.transform.position = startingPoint.position;

        while (Vector3.Distance(_visualBody.transform.position, endPoint.position) > 0.01f)
        {
            var movement = Vector3.Lerp(startingPoint.position, endPoint.position, time) - transform.position;
            transform.Translate(movement);
            time += Time.deltaTime;
            yield return null;
        }

        MenuManager.instance.InteractionsInCooldown(false);
    }*/

    private void OnDestroy()
    {
        EventManager.Unsubscribe(UpdateController, Events.ChangePerspective);
    }

    private void OnDrawGizmos()
    {
        #region GROUND_COLLIDER
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundPoint.position, _groundedRadius);
        #endregion
    }
}
