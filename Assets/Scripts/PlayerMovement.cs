using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float rotationSpeed = 5.0f;

    Vector3   moveDirection;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        GatherInput();
        MovePlayer();
    }

    private void GatherInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
    }

    private void MovePlayer()
    {
        Vector3 velocity = new(rb.velocity.x, 0.0f, rb.velocity.z);
        if (velocity.magnitude > moveSpeed)
        {
            velocity = velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(velocity.x, 0.0f, velocity.z);
        }
        rb.AddForce(moveSpeed * 10 * moveDirection, ForceMode.Force);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
