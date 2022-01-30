using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody2D _riggidRigidbody2D;
    public Animator animator;

    public float speed = 12.0f;
    public float turnSmoothTime = 0.1f;

    //private MasterInput _controls;
    private Vector2 _playerInputDirection;
    private float _turnSmoothVelocity;

    public bool isDashing;

    private void Awake()
    {
        _riggidRigidbody2D = GetComponent<Rigidbody2D>();
        //_controls = new MasterInput();
        //_controls.Player.Move.started += context => _playerInputDirection = context.ReadValue<Vector2>();
        //_controls.Player.Move.performed += context => _playerInputDirection = context.ReadValue<Vector2>();
        //_controls.Player.Move.canceled += context => _playerInputDirection = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            animator.SetBool("dash", true);
        }
        else
        {
            animator.SetBool("dash", false);
            MovePlayer(_playerInputDirection);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _playerInputDirection = ctx.ReadValue<Vector2>();
    }

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

            animator.SetFloat("velocity", 1f);
        }
        else
        {
            _riggidRigidbody2D.velocity = Vector2.zero;
            animator.SetFloat("velocity", 0f);
        }
    }

    private void OnEnable()
    {
        //_controls.Enable();
    }

    private void OnDisable()
    {
        //_controls.Disable();
    }
}