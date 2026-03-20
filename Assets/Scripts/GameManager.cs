using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coins = 0;
    public TextMeshProUGUI coinText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddCoin()
    {
        coins++;
        UpdateUI();
    }

    void UpdateUI()
    {
        coinText.text = "Monedas: " + coins;
    }
}