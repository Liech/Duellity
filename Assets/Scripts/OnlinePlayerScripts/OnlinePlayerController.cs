using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class OnlinePlayerController : MonoBehaviourPunCallbacks
{
    private Rigidbody2D _riggidRigidbody2D;
    public Animator _animator;
    public float speed = 12.0f;
    public float turnSmoothTime = 0.1f;
    MasterInput controls;
    private Vector2 _playerInputDirection;
    private float _turnSmoothVelocity;
    PhotonView PV;

    public bool Stun = false;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        _riggidRigidbody2D = GetComponent<Rigidbody2D>();
        controls = new MasterInput();
        controls.Player.Move.started += context => _playerInputDirection = context.ReadValue<Vector2>();
        controls.Player.Move.performed += context => _playerInputDirection = context.ReadValue<Vector2>();
        controls.Player.Move.canceled += context => _playerInputDirection = context.ReadValue<Vector2>();
    }
    void Start()
    {
        if (!PV.IsMine)
        {
            
        }
    }
    private void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        _playerInputDirection = controls.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if (!Stun)
            MovePlayer(_playerInputDirection);
    }

    //public void OnMove(InputAction.CallbackContext ctx)
    //{
    //    _playerInputDirection = ctx.ReadValue<Vector2>();
    //}
    private void MovePlayer(Vector2 movementDirection)
    {
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        var viewCorrectedDirection = new Vector2(-movementDirection.x, movementDirection.y);

        var unnormalized = new Vector2(viewCorrectedDirection.x, viewCorrectedDirection.y);
        var direction = unnormalized.normalized;
        if (unnormalized.magnitude >= 0.05f)
        {
            var targetAngle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
            var smoothedAngle =
                Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, 0f, smoothedAngle);
            var motion = direction * speed * Time.deltaTime;
            _riggidRigidbody2D.velocity = motion;


            var velocityY = Vector3.Dot(motion.normalized, transform.forward);
            var velocityX = Vector3.Dot(motion.normalized, transform.right);

            _animator.SetFloat("velocity", 1f);
        }
        else
        {
            _riggidRigidbody2D.velocity = Vector2.zero;
            _animator.SetFloat("velocity", 0);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}