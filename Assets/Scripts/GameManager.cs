using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public Immortality immortality;
    public Magnet magnet;

    public static GameManager instance;

    public Text scoreText;

    public bool inGame = true;

    public Text EndScore;

    public float worldScrollingSpeed = 0.2f;

    private float score = 0f;

    public Text coinsText;

    private int coins;

    private int highScore;

    public Text highScoreText;

    private void Start()
    {
        inGame = false;

        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        if (instance == null)
        {
            instance = this;
        }
        inGame = false;
        Initialize();
    }
    private void FixedUpdate()
    {
        if (!inGame) return;
        score += worldScrollingSpeed;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString("0");
        coinsText.text = coins.ToString("");
        highScoreText.text = highScore.ToString("0");
    }

    public void Initialize()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            coins = 0;
            PlayerPrefs.SetInt("Coins", 0);
        }

        if (PlayerPrefs.HasKey("HighScoreValue"))
        {
            highScore = PlayerPrefs.GetInt("HighScoreValue");
        }
        else
        {
            highScore = 0;
            PlayerPrefs.SetInt("HighScoreValue", 0);
        }
        gameOverPanel.SetActive(false);
        inGame = true;
        immortality.isActive = false;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        inGame = false;
        scoreText.enabled = false;
        EndScore.enabled = true;

        EndScore.text = score.ToString("0");

        if ((int)score > highScore)
        {
            Debug.Log(score);
            highScore = (int)score;
            PlayerPrefs.SetInt("HighScoreValue", highScore);

        }
    }

    public void CoinCollected(int value = 1)
    {
        coins += value;
        PlayerPrefs.SetInt("Coins", coins);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ImmortalityCollected()
    {
        if(immortality.isActive)
        {
            CancelInvoke("CancelImmortality");
            CancelImmortality();
        }
        immortality.isActive = true;
        worldScrollingSpeed += immortality.GetSpeedBoost();
        Invoke("CancelImmortality", immortality.GetDuration());
    }

    public void MagnetCollected()
    {
        if (magnet.isActive)
        {
            CancelInvoke("CancelMagnet");
        }
        magnet.isActive = true;
        Invoke("CancelMagnet", magnet.GetDuration());
    }

    private void CancelMagnet()
    {
        magnet.isActive = false;
    }

    public void CancelImmortality()
    {
        immortality.isActive = false;
        worldScrollingSpeed -= immortality.GetSpeedBoost();

    }


}
