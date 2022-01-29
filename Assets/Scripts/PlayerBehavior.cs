using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 8.0f;
    public float turnSmoothTime = 0.1f;

    private MasterInputs _controls;
    private Vector2 _playerInput;
    private float _turnSmoothVelocity;

    private void Awake()
    {
        _controls = new MasterInputs();
        _controls.Player.Movement.performed += context => _playerInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovePlayer(_playerInput);
    }

    private void MovePlayer(Vector2 movementDirection)
    {
        var direction = new Vector3(movementDirection.x, 0f, movementDirection.y).normalized;
        if (direction.magnitude >= 0.1f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var smoothedAngle =
                Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
            characterController.Move(direction * speed * Time.deltaTime);
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