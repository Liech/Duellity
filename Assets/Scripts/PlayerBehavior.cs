using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerBehavior : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    public float speed = 12.0f;
    public float turnSmoothTime = 0.1f;

    private MasterInput _controls;
    private Vector2 _playerInputDirection;
    private float _turnSmoothVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _controls = new MasterInput();
        _controls.Player.Move.started += context => _playerInputDirection = context.ReadValue<Vector2>();
        _controls.Player.Move.performed += context => _playerInputDirection = context.ReadValue<Vector2>();
        _controls.Player.Move.canceled += context => _playerInputDirection = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovePlayer(_playerInputDirection);
    }

    private void MovePlayer(Vector2 movementDirection)
    {
        var viewCorrectedDirection = new Vector2(movementDirection.x, -movementDirection.y);

        var direction = new Vector3(viewCorrectedDirection.x, viewCorrectedDirection.y, 0f).normalized;
        if (direction.magnitude >= 0.05f)
        {
            var targetAngle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
            var smoothedAngle =
                Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, 0f, smoothedAngle);
            var motion = direction * speed * Time.deltaTime;
            _characterController.Move(motion);

            var velocityY = Vector3.Dot(motion.normalized, transform.forward);
            var velocityX = Vector3.Dot(motion.normalized, transform.right);

            _animator.SetFloat("VelocityY", velocityY, 0.1f, Time.deltaTime);
            _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}