using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private float m_horizontalInput;
    private float m_verticalInput;

    private Vector3 m_moveDirection;

    private Rigidbody m_rb;
    private Vector3 m_limitedVelocity;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.freezeRotation = true;
    }

    // Update is called once per frame
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
        m_horizontalInput = Input.GetAxisRaw("Horizontal");
        m_verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        Vector3 m_velocity = new Vector3(m_rb.velocity.x, 0.0f, m_rb.velocity.z);

        m_moveDirection = (transform.forward * m_verticalInput) + (transform.right * m_horizontalInput);
        if (m_velocity.magnitude > moveSpeed)
        {
            m_limitedVelocity = m_velocity.normalized * moveSpeed;
            m_rb.velocity = new Vector3(m_limitedVelocity.x, 0.0f, m_limitedVelocity.z);
        }
        if (m_moveDirection.magnitude > 1) 
        {
            m_moveDirection = m_moveDirection.normalized;
        }
        m_rb.AddForce(moveSpeed * 10 * m_moveDirection, ForceMode.Force);
        Debug.Log(m_moveDirection);
        Debug.Log(m_limitedVelocity);
    }
}
