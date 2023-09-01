using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private float m_horizontalInput;
    private float m_verticalInput;

    Vector3 m_moveDirection;

    Rigidbody m_rb;

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
        m_moveDirection = (transform.forward * m_verticalInput) + (transform.right * m_horizontalInput);
        m_rb.AddForce(moveSpeed * 10 * m_moveDirection.normalized, ForceMode.Force);
    }
}
