using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 7f;
    public float jumpDelay = 1.5f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    private float direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("Jump", jumpDelay, jumpDelay);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // cambiar dirección al chocar
        direction *= -1;
        Flip();
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}