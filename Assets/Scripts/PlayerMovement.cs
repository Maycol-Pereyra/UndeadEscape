using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Animación
    Animator animator;

    // Movimiento
    public float speed = 5f;
    public float jumpForce = 7f;

    // Vidas
    public TextMeshProUGUI livesText;
    public int lives = 3;

    public float knockbackForce = 5f;
    public float knockbackUpForce = 5f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isKnockback = false;

    public GameObject deathEffect;
    public GameOverManager gameOverManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        UpdateLivesUI();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        if (isKnockback)
        {
            return;
        }

        float move = Input.GetAxis("Horizontal");

        // Movimiento
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Animación
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Girar personaje
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Detectar suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Detectar enemigo (daño lateral)
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            int damage = collision.gameObject.CompareTag("Enemy")
                ? 1
                : 2;

            float knockStrength = collision.gameObject.CompareTag("Enemy")
                ? 2.5f
                : 4f;

            // Solo recibe daño si NO está cayendo
            if (rb.linearVelocity.y >= 0)
            {
                lives -= damage;
                UpdateLivesUI();

                // Dirección del empuje
                Vector2 knockDirection = new Vector2(
                    transform.position.x > collision.transform.position.x ? 1 : -1,
                    1
                );

                Knockback(knockDirection * knockStrength);

                if (lives <= 0)
                {
                    Die();
                }
            }
        }

        // Detectar fuego
        if (collision.gameObject.CompareTag("Fire"))
        {
            lives = 0;
            UpdateLivesUI();
            Die();
        }
    }

    void Die()
    {

        SoundManager.instance.PlaySound(SoundManager.instance.playerDieSound);

        Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(transform.gameObject);

        gameOverManager.ShowGameOver();
    }

    void UpdateLivesUI()
    {
        livesText.text = "Vidas: " + lives;
    }

    void Knockback(Vector2 direction)
    {
        isKnockback = true;

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(direction, ForceMode2D.Impulse);

        Invoke("ResetKnockback", 0.3f);
    }

    void ResetKnockback()
    {
        isKnockback = false;
    }
}