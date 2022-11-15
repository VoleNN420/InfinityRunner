using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject upgradeStorePanel;

    public Text magnerLevelText;
    public Text magnetButtonText;

    public Text immortalityLevelText;
    public Text immortalityButtonText;

    public Powerup magnet;
    public Powerup immortality;

    public Text highScoreText;
    public Text CoinsText;
    public Text soundBtnText;

    int highScoreValue = 0;
    int coinsAmount = 0;

    private void Start()
    {
        Screen.SetResolution(1600, 1000, false);
        if (PlayerPrefs.HasKey("HighScoreValue"))
        {
            highScoreValue = PlayerPrefs.GetInt("HighScoreValue");
        }
        if (PlayerPrefs.HasKey("Coins"))
        {
            coinsAmount = PlayerPrefs.GetInt("Coins");
        }

        mainMenuPanel.SetActive(true);
        upgradeStorePanel.SetActive(false);
        UpdateUI();
    }

    public void UpdateUI()
    {
        highScoreText.text = $"HS: {highScoreValue.ToString()}";
        CoinsText.text = $"Coins: {coinsAmount.ToString()}";
        if (SoundManager.instance.GetMuted())
        {
            soundBtnText.text = "Turn on sound";
        }
        else
        {
            soundBtnText.text = "Turn off sound";
        }

        immortalityLevelText.text = immortality.ToString();
        immortalityButtonText.text = immortality.UpgradeCostString();

        magnerLevelText.text = magnet.ToString();
        magnetButtonText.text = magnet.UpgradeCostString();
    }

    public void UpgradeImmortalityButton()
    {
        UpgradePowerup(immortality);
    }
    public void UpgradeMagnetButton()
    {
        UpgradePowerup(magnet);
    }

    private void UpgradePowerup(Powerup powerup)
    {
        if (coinsAmount >= powerup.GetNextUpgradeCost())
        {
            ReduceCoinsAmount(powerup.GetNextUpgradeCost());
            powerup.Upgrade();
            UpdateUI();
        }
    }
    private void ReduceCoinsAmount(int amount)
    {
        coinsAmount -= amount;
        PlayerPrefs.SetInt("Coins", coinsAmount);
    }


    public void PlayButton()
    {
        SoundManager.instance.PlayClickSound();
        SceneManager.LoadScene("Game");
    }

    public void SoundButton()
    {
        SoundManager.instance.ToggleButton();
        SoundManager.instance.PlayClickSound();
        UpdateUI();
    }

    public void OpenUpgradeStore()
    {
        SoundManager.instance.PlayClickSound();
        mainMenuPanel.SetActive(false);
        upgradeStorePanel.SetActive(true);
    }

    public void CloseUpgradeStore()
    {
        mainMenuPanel.SetActive(true);
        upgradeStorePanel.SetActive(false);
    }

}
