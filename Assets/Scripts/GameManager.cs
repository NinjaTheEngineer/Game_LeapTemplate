using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AdModScript ads;

    public PlayerPrefManagerScript prefsScript;
    public Material[] skins;
    public Material selectedSkin;

    public int coinsCollected;
    enum RampPosition { LEFT, MID, RIGHT }
    RampPosition pos;
    RampPosition secondPos;

    public SceneFader sceneFader;
    public GameObject coinPrefab;

    public GameObject coinsInGameObject;
    public GameObject coinsInLobbyObject;
    public GameObject highscoreInGameObject;
    public GameObject highscoreInLobbyObject;
    public GameObject highScoreMark;

    public TextMeshProUGUI coinsInGame;
    public TextMeshProUGUI coinsInLobby;
    public TextMeshProUGUI highscoreInGame;
    public TextMeshProUGUI highscoreInLobby;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI highScoreMarkText;

    public Transform highscoreStar;

    [SerializeField] private float spawnerSpeed = 1f;
    [SerializeField] private float spawnerMaxDistance = 2f;
    [SerializeField] private float timePositionChange = 1f;

    private SpawnerScript spawnerScript;
    [SerializeField] private GameObject spawner;

    [SerializeField] private float timer = 0f;
    [SerializeField] private float timeBetweenSpawns = 1f;

    [SerializeField] private int count = 0;

    [SerializeField] private bool gameStarted = false;

    private PlayerPrefSkins[] savedSkins;

    public GameObject normalRamp;

    public int score = 0;
    public int highscore= 0;
    public bool hasPassedHighscore = false;
    private bool at10 = false;
    private bool at100 = false;
    private bool at1000 = false;
    private bool pass10 = false;
    private bool pass100 = false;
    private bool pass1000 = false;
    private bool bannerOn = false;
    private int collectingCoins;
    void Start()
    {
        collectingCoins = 0;
        coinsInGameObject.SetActive(false);
        spawnerScript = spawner.GetComponent<SpawnerScript>();
        coinsCollected = prefsScript.GetPrefCoins();
        UpdateCoinsUI();
        highscore = PlayerPrefs.GetInt("highscore");
        CheckHighscoreStart();
        highscoreInLobby.text = highscore.ToString();
        highscoreInGame.text = highscore.ToString();
        score = 0;
        currentScore.text = "";
        //ads.RequestBanner();
    }

    void Update()
    {
        if (!gameStarted)
        {
            return;
        }
        if (!bannerOn)
        {
            //ads.ShowBannerAd();
            bannerOn = true;
        }
        SpawnerActivated();
    }

    void CheckHighscoreStart()
    {
        if (highscore > 999)
        {
            at1000 = true;
        }
        else if (highscore > 99)
        {
            at100 = true;
        }
        else if (highscore > 9)
        {
            at10 = true;
        }
        PositionStar();
    }
    void HighscorePass()
    {
        if (highscore.Equals(1000))
        {
            pass1000 = true;
        }
        else if (highscore.Equals(100))
        {
            pass100 = true;
        }
        else if (highscore.Equals(10))
        {
            pass10 = true;
        }
        PositionStarPass();
    }
    void PositionStar()
    {
        if (at10)
        {
            highscoreStar.position = new Vector3(highscoreStar.position.x - 40f, highscoreStar.position.y, highscoreStar.position.z);
            at10 = false;
        }else if (at100)
        {
            highscoreStar.position = new Vector3(highscoreStar.position.x - 40f*2, highscoreStar.position.y, highscoreStar.position.z);
            at100 = false;
        }
        else if(at1000)
        {
            highscoreStar.position = new Vector3(highscoreStar.position.x - 40f*3, highscoreStar.position.y, highscoreStar.position.z);
            at1000 = false;
        }
    }
    void PositionStarPass()
    {
        if (pass10)
        {
            highscoreStar.position = new Vector3(highscoreStar.position.x - 40f, highscoreStar.position.y, highscoreStar.position.z);
            pass10 = false;
        }
        else if (pass100)
        {
            highscoreStar.position = new Vector3(highscoreStar.position.x - 40f, highscoreStar.position.y, highscoreStar.position.z);
            pass100 = false;
        }
        else if (pass1000)
        {
            highscoreStar.position = new Vector3(highscoreStar.position.x - 40f, highscoreStar.position.y, highscoreStar.position.z);
            pass1000 = false;
        }
    }
    void UpdateCoinsUI()
    {
        coinsInGame.text = coinsCollected.ToString();
        coinsInLobby.text = coinsCollected.ToString();
    }

    void UpdateScore()
    {
        if (!hasPassedHighscore)
        {
            currentScore.text = score.ToString();
        }
        else
        {
            currentScore.text = "";
            highscore = score;
            highScoreMark.SetActive(true);
            highscoreInGameObject.SetActive(false);
            highScoreMarkText.text = score.ToString();
            HighscorePass();
        }
    }

    void SpawnerActivated()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawns)
        {
            PickPosition();
            SpawnNormalRamp(pos, secondPos);
            count++;
        }
    }

    public void GoToSkins()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        sceneFader.FadeTo("SkinsSelector");
    }

    public float rampRange = 0.1f;
    void SpawnNormalRamp(RampPosition pos, RampPosition pos2)
    {
        var posX = spawner.transform.position.x;
        var posY = spawner.transform.position.y;
        var posZ = spawner.transform.position.z;

        var rotation = Quaternion.Euler(normalRamp.transform.rotation.x, spawnerScript.GetAngleDir(), normalRamp.transform.rotation.z);
        if (pos == RampPosition.LEFT || pos2 == RampPosition.LEFT)
        {
            Instantiate(normalRamp, new Vector3(Random.Range(posX + (-2f + rampRange), posX + (-1.4f + rampRange)), posY, posZ), rotation);
        }
        if (pos == RampPosition.MID || pos2 == RampPosition.MID)
        {
            Instantiate(normalRamp, new Vector3(Random.Range(posX + (-rampRange), posX + rampRange), posY, posZ), rotation);
        }
        if (pos == RampPosition.RIGHT || pos2 == RampPosition.RIGHT)
        {
            Instantiate(normalRamp, new Vector3(Random.Range(posX + 1.2f + rampRange, posX +1.8f + rampRange),  posY, posZ), rotation);
        }
        timer = 0f;
    }

    void PickPosition()
    {
        pos = GetRandomEnum<RampPosition>();
        secondPos = GetRandomEnum<RampPosition>();
        while(secondPos == pos)
        {
            secondPos = GetRandomEnum<RampPosition>();
        }
    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public float GetSpawnerSpeed()
    {
        return spawnerSpeed;
    }
    public float GetSpawnerMaxDistance()
    {
        return spawnerSpeed;
    }

    public float GetTimeBetweenPositionChange()
    {
        return timePositionChange;
    }

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        gameStarted = true;
        coinsInGameObject.SetActive(true);
        coinsInLobbyObject.SetActive(false);
        highscoreInGameObject.SetActive(true);
        highscoreInLobbyObject.SetActive(false);
    }

    public void StopGame()
    {
        FindObjectOfType<AudioManager>().PlaySound("Death");
        gameStarted = false;
        coinsInGameObject.SetActive(false);
        coinsInLobbyObject.SetActive(true);
        highscoreInGameObject.SetActive(false);
        highscoreInLobbyObject.SetActive(true);
    }

    public bool GameOnline()
    {
        return gameStarted;
    }

    public Material GetSelectedSkin()
    {
        savedSkins = prefsScript.GetSkinsFromPrefs();
        foreach (PlayerPrefSkins skin in savedSkins)
        {
            if (skin.value.Equals(2))
            {
                ChooseSkin(skin.name);
            }
        }
        return selectedSkin;
    }

    private void ChooseSkin(string name)
    {
        switch (name)
        {
            case "red":
                selectedSkin = skins[1];
                break;
            case "pink":
                selectedSkin = skins[2];
                break;
            case "black":
                selectedSkin = skins[3];
                break;
            case "glass":
                selectedSkin = skins[4];
                break;
            case "gold":
                selectedSkin = skins[5];
                break;
            case "reflex":
                selectedSkin = skins[6];
                break;
            default:
                selectedSkin = skins[0];
                break;
        }
    }
    public void CollectCoin()
    {
        collectingCoins++;
        coinsCollected++;
        PlayerPrefs.SetInt("coins", coinsCollected);
        UpdateCoinsUI();
    }

    public void IncreaseScore()
    {
        score++;
        currentScore.text = score.ToString();
        if(score > highscore)
        {
            hasPassedHighscore = true;
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }
        UpdateScore();
    }

    public int GetFinalScore()
    {
        return score;
    }

    public int GetCollectedCoins()
    {
        return collectingCoins;
    }
}
