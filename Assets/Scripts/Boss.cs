using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 5;
    public float speed = 2f;

    public Transform groundCheck;
    public float checkDistance = 0.7f;
    public LayerMask groundLayer;

    private int direction = 1;
    private Rigidbody2D rb;

    private bool isGroundAhead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();

        if (isGroundAhead)
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        }
        else
        {
            // mover un poco hacia atrás antes de girar
            transform.position += new Vector3(-direction * 0.3f, 0, 0);

            Flip();
        }
    }

    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);

        isGroundAhead = hit.collider != null;
    }

    void Flip()
    {
        direction *= -1;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // mover el detector al otro lado
        Vector3 pos = groundCheck.localPosition;
        pos.x *= -1;
        groundCheck.localPosition = pos;
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}