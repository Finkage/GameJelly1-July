using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private float moveSpeedCopy;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        moveSpeedCopy = moveSpeed;
    }

    private void Update()
    {
        if (ViewMap.mapIsUp)
        {
            // Stop player movement if mid-movement
            moveSpeed = 0;
            rb.velocity = Vector3.zero;
            return;
        }

        UpdateInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void UpdateInput()
    {
        moveSpeed = moveSpeedCopy;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
