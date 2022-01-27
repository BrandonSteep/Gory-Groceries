using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public AnimationCurve moveCurve;
    public float moveTime;
    public float moveMultiplier = 10f;
    public float groundDrag = 6f;

    float horizontalMovement;
    float verticalMovement;

    [SerializeField] Headbob bob;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    public bool isGrounded;
    float groundDistance = 0.4f;
    public bool isFalling;

    [Header("Jump")]
    public float jumpForce = 5f;
    public float airMultiplier = 3f;
    public float airDrag = 1.5f;
    //Better Jump//
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    RaycastHit slopeHit;

    bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    //References
    public Rigidbody rb;
    [SerializeField] Transform orientation;
    [SerializeField] PhysicMaterial physicMaterial;

    public ParticleSystem dustFX;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        bob = GetComponentInParent<Headbob>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);


        PlayerInput();
        ControlDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        bob.Bob(isGrounded);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        BetterJump();
    }

    void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.transform.forward * verticalMovement + orientation.transform.right * horizontalMovement;

    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        bob.PlayJumpSound();
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0 && rb.velocity.y > -20f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            isFalling = true;
        }
        else if (rb.velocity.y < -20f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            if (isFalling == isGrounded)
            {
                PlayDust();
                isFalling = false;
            }
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && !OnSlope())
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void MovePlayer()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            moveTime += Time.deltaTime;
        }
        else
        {
            moveTime = 0;
        }

        moveSpeed = moveCurve.Evaluate(moveTime);

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
            physicMaterial.dynamicFriction = 0f;
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
            physicMaterial.dynamicFriction = 1f;
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Acceleration);
            physicMaterial.dynamicFriction = 0f;
        }
    }

    public void Knockback(float force)
    {
        rb.AddForce(Camera.main.transform.forward * -force, ForceMode.Impulse);
    }

    void PlayDust()
    {
        bool dustPlayed = false;
        while (!dustPlayed)
        {
            dustFX.Play();
            bob.PlayLandingSound();
            dustPlayed = true;
        }
    }
}
