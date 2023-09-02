using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    Vector3 limitedVelocity;

    float horizontalInput;
    float verticalInput;

    public Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GatherInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        Vector3 Velocity = new(rb.velocity.x, 0.0f, rb.velocity.z);

        moveDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
        if (Velocity.magnitude > moveSpeed)
        {
            limitedVelocity = Velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, 0.0f, limitedVelocity.z);
        }
        if (moveDirection.magnitude > 1) 
        {
            moveDirection = moveDirection.normalized;
        }
        rb.AddForce(moveSpeed * 10 * moveDirection, ForceMode.Force);
    }
}