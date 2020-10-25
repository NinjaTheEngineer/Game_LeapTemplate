using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI totalText;
    private int score;
    private int coins;

    private void Awake()
    {
        score = gameManager.GetFinalScore();
        coins = gameManager.GetCollectedCoins();
    }
    private void Start()
    {
        scoreText.text = score.ToString();
        coinsText.text = coins.ToString();
        var total = score + coins * 5;
        totalText.text = total.ToString();
    }
}
