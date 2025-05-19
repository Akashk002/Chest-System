using UnityEngine;
using TMPro;

public class CurrencyHandler
{
    private int coin;
    private int gem;
    private TMP_Text coinText;
    private TMP_Text gemText;

    public CurrencyHandler(TMP_Text coinText, TMP_Text gemText)
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
        gem = PlayerPrefs.GetInt("Gem", 0);

        this.coinText = coinText;
        this.coinText.SetText(coin.ToString());

        this.gemText = gemText;
        this.gemText.SetText(gem.ToString());
    }

    public int GetCoin()
    {
        return coin;
    }
    public int GetGem()
    {
        return gem;
    }
    public void AddCoin(int value)
    {
        coin += value;
        PlayerPrefs.SetInt("Coin", coin);
        coinText.SetText(coin.ToString());
    }
    public void AddGems(int value)
    {
        gem += value;
        SetGems();
    }

    public void SpendGems(int value)
    {
        gem -= value;
        if (gem < 0)
        {
            gem = 0;
        }
        SetGems();
    }

    public void SetGems()
    {
        PlayerPrefs.SetInt("Gem", gem);
        gemText.SetText(gem.ToString());
    }
}
