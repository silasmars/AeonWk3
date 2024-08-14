using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCustom : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float jumpPower;
    public float gravity;

    public float speedThisFrame;

    public Vector2 inputThisFrame;

    public Vector3 movementThisFrame;

    public Rigidbody rb;

    public LayerMask groundedMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputThisFrame.x = Input.GetAxis("Horizontal");
        inputThisFrame.y = Input.GetAxis("Vertical");

        movementThisFrame = Vector3.zero;

        movementThisFrame.x = inputThisFrame.x;
        movementThisFrame.z = inputThisFrame.y;

        speedThisFrame = walkSpeed;

        if (Input.GetButton("Sprint")) ;
        {
            speedThisFrame = runSpeed;
        }

        if (Input.GetButton("Crouch")) ;
        {
            speedThisFrame = crouchSpeed;
        }

        movementThisFrame *= speedThisFrame;

        movementThisFrame.y = rb.velocity.y - gravity * Time.deltaTime;

        if (IsGrounded())
        {
            if (Input.GetButton("Jump"))
            {
                movementThisFrame.y = jumpPower;
            }
        }

        Move(movementThisFrame);
    }

    private void Move(Vector3 movement)
    {
        rb.velocity = movement;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.05f, groundedMask);
    }
}
