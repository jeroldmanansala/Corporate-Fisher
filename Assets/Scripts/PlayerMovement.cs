using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    public float moveSpeed = 5f;
    bool isFacingRight = true;
    public float jumpPower = 5f;
    bool isGrounded = false;

    Rigidbody2D rb;
    Animator animator;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        FlipSprite();

        // Ground check (do this early so jump uses correct isGrounded)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump when grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

        // Set jumping anim based on current state
        animator.SetBool("IsJumping", !isGrounded);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        animator.SetFloat("XVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("YVelocity", rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if ((horizontalInput > 0 && !isFacingRight) || (horizontalInput < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
