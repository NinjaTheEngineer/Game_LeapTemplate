using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeadScreenScript : MonoBehaviour
{
    public AdModScript ads;

    public SceneFader sceneFader;
    public Transform player;
    public GameObject deadScreen;
    public GameObject pauseButton;
    public ProgressBarScript progressBar;
    public TextMeshProUGUI scoreText;
    public GameObject highScore;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject noThanks;

    public static DeadScreenScript instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        ads = FindObjectOfType<AdModScript>().GetComponent<AdModScript>();
    }
    public void PlayerIsDead()
    {
        pauseButton.SetActive(false);
        deadScreen.SetActive(true);
        Time.timeScale = 0f;
        scoreText.text = "";
        highScore.SetActive(false);
        progressBar.SetStartProgressBar(true);
    }

    public void GameOver()
    {
        Time.timeScale = 1f;
        deadScreen.SetActive(false);
        pauseButton.SetActive(false);
        gameOverScreen.SetActive(true);
        scoreText.text = "";
        highScore.SetActive(false);
        
    }
    public void PlayerRestart()
    {
        //ads.RequestRewardedBaseVideo();
        ContinueGame();
    }
    public void ContinueGame()
    {
        //pauseButton.SetActive(true);
        pauseScreen.SetActive(true);
        deadScreen.SetActive(false);
        player.position = new Vector3(player.transform.position.x, 4.5f, player.transform.position.z);
        //Time.timeScale = 1f;
    }
    public void BackToMainMenu()
    {
        player.position = new Vector3(player.transform.position.x, 112.5f, player.transform.position.z);
        Time.timeScale = 1f;
        sceneFader.FadeTo("MainMenu");
    }

    public void Restart()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        //ads.RequestInterstitial();
    }

    public void NoThanksActive()
    {
        noThanks.SetActive(true);
    }

    public void ShowRewardedAd()
    {
        ads.ShowRewardedAd();
    }
}
