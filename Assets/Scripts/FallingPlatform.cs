using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1.5f;

    private Rigidbody2D rb;
    private bool isPlayerOn = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = true;
            Invoke("Fall", fallDelay);
        }
    }

    void Fall()
    {
        if (isPlayerOn)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = false;
            CancelInvoke("Fall");
        }
    }
}