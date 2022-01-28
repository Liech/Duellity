using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public CharacterController CharacterController;
    
    public float speed = 6.0f;
    public float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            MovePlayer(direction);
        }
    }

    private void MovePlayer(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var smoothedAngle =
            Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
        CharacterController.Move(direction * speed * Time.deltaTime);
    }
}