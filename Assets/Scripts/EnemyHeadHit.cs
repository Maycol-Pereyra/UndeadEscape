using UnityEngine;

public class EnemyHeadHit : MonoBehaviour
{
    public GameObject deathEffect;

    public float bounceForce = 8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

            // SIEMPRE rebota
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);

            // destruir enemigo completo (no solo la cabeza)
            Vector3 spawnPosition = transform.position;
            spawnPosition.y -= 0.7f;

            SoundManager.instance.PlaySound(SoundManager.instance.enemyDieSound);

            Instantiate(deathEffect, spawnPosition, Quaternion.identity);

            Destroy(transform.parent.gameObject);
        }
    }
}