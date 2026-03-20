using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Boss boss = collision.GetComponent<Boss>();

        if (boss != null)
        {
            boss.TakeDamage();
            Destroy(gameObject);
        }

        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}