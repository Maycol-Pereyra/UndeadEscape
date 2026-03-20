using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting player = collision.GetComponent<PlayerShooting>();

            if (player != null)
            {
                player.hasGun = true;
            }

            Destroy(gameObject);
        }
    }
}