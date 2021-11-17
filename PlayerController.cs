using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float gravityScale;

    public LayerMask groundLayer;
    public float JumpForce = 10;
    public float MoveSpeed = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 velocity;
    private float rawY = 0;
    private void FixedUpdate()
    {
        // Get x-input
        float xInput = Input.GetAxisRaw("Horizontal");
        velocity.x = xInput * MoveSpeed;

        // Change gravity scale based on player state
        if (rb.velocity.y < 0)
            gravityScale = 0.2f;
        else
            gravityScale = 0.1f;

        // Add gravity
        rawY += Physics2D.gravity.y * gravityScale;
        velocity.y = Mathf.Clamp(rawY, -15, 15);
        if((int)rb.velocity.y == 0)
        {
            rawY = 0;
        }

        // Set velocity
        rb.velocity = velocity;

        if(IsGrounded() && Input.GetAxisRaw("Vertical") == 1)
        {
            Jump();
        }
    }

    // Jump function
    void Jump()
    {
        rawY = JumpForce;
    }

    // Is grounded code (altered from https://kylewbanks.com/blog/unity-2d-checking-if-a-character-or-object-is-on-the-ground-using-raycasts)
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.9f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.transform != null)
        {
            return true;
        }

        return false;
    }
}
