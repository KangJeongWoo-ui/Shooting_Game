using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private Text coinText;

    [SerializeField] private PlayerInventory playerInventory;

    private int coin = 0;

    private void OnEnable()
    {
        playerInventory.OnCoinCount += UpdateCoin;
    }
    private void OnDisable()
    {
        playerInventory.OnCoinCount -= UpdateCoin;
    }
    private void Update()
    {
        coin = playerInventory.coinCount;

        UpdateCoin(coin);
    }
    private void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
