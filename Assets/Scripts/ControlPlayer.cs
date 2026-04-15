using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundCheck groundCheck;

    [Header("// MOVE -----------------------------------------------------------------------------------------")]
    [SerializeField] private float moveForce;
    private float horizontal;
    [SerializeField] private float rotationDamp;
    [SerializeField] private float brakeForce;
    private bool isBraking;

    [Header("// JUMP -----------------------------------------------------------------------------------------")]
    [SerializeField] private float jumpForce;
    private bool grounded;
    [SerializeField] private float coyoteTime;
    private float coyoteTimer;
    [SerializeField] private float jumpBufferTime;
    private float jumpBufferCounter;
    [Space(10)]
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    

    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!groundCheck) groundCheck = GetComponent<GroundCheck>();
    }
    
    void Update()
    {
        // GET INPUT ----------------------------
        horizontal = Input.GetAxis("Horizontal");
        isBraking = Input.GetButton("Brake");
        grounded = groundCheck.GetIsGrounded();


        // JUMP ---------------------------------
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;

        // COYOTE TIME
        if (grounded) { coyoteTimer = coyoteTime; }
        else { coyoteTimer -= Time.deltaTime; }

        // JUMP BUFFER
        if (jumpBufferCounter > 0) jumpBufferCounter -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (grounded)
        { Move(); }

        if (grounded && isBraking)
        { Braking(); }

        if (jumpBufferCounter > 0 && coyoteTimer > 0f)
        { Jump(); }
        BetterCallJump();


        // ROTATION SETTINGS // / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /
        if (grounded)
        {
            bool movingOpposite = horizontal * rb.velocity.x < 0;
            if (!isBraking || movingOpposite)
            { rb.AddTorque(-horizontal * moveForce); }
        }
        // ROTATION BRAKE SMOTH ON THE GROUND
        if (grounded && Mathf.Abs(horizontal) < 0.1f && isBraking)
        { rb.angularVelocity *= rotationDamp; }

        // ROTATION SMOTH IN THE AIR
        if (!grounded)
        { rb.angularVelocity *= rotationDamp; }
    }



    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-

    private void Move()  // MOVE // / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /
    {
        rb.AddForce(Vector2.right * horizontal * moveForce);
    }


    private void Braking()  // BRAKING BAD // / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 
    {
        float brake = -Mathf.Sign(rb.velocity.x) * brakeForce * Time.fixedDeltaTime;
        rb.AddForce(Vector2.right * brake);
    }


    private void Jump()  // JUMP // / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /
    {
        // COYOTE TIME AND JUMP BUFFER
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        jumpBufferCounter = 0f;
        coyoteTimer = 0f;
    }
    private void BetterCallJump()  // FALLING ACCELERATION
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
}