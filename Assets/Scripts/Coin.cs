using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.coinSound);

            GameManager.instance.AddCoin();

            Destroy(gameObject);
        }
    }
}