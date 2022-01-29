using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehavior : MonoBehaviour
{
    private CharacterController _characterController;

    public float speed = 12.0f;
    public float turnSmoothTime = 0.1f;

    private MasterInput _controls;
    private Vector2 _playerInputDirection;
    private float _turnSmoothVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
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
        var direction = new Vector3(movementDirection.x, 0f, movementDirection.y).normalized;
        if (direction.magnitude >= 0.05f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var smoothedAngle =
                Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
            _characterController.Move(direction * speed * Time.deltaTime);
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