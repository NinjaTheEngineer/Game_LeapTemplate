using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPrefManagerScript : MonoBehaviour
{
    public PlayerPrefSkins[] skins;
    public int coinsCollected;
    public GameObject error;
    public TextMeshProUGUI coins;

    public static PlayerPrefManagerScript instance = null;
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
    public void Start()
    {
        //PlayerPrefs.SetInt("prefsCreated", 0);
        skins = GetPrefSkins();
        SaveLocalToPrefs();
        coinsCollected = GetPrefCoins();
        coins.text = coinsCollected.ToString();
    }
    public void SetValue(int index, int value)
    {
        if (value.Equals(2))
        {
            foreach (PlayerPrefSkins i in skins)
            {
                if (i.value.Equals(2))
                {
                    i.value = 1;
                    PlayerPrefs.SetInt(i.name, i.value);
                }
            }
        } 

        skins[index].value = value;

        PlayerPrefs.SetInt(skins[index].name, skins[index].value);
    }
    public PlayerPrefSkins[] GetSkinsFromPrefs()
    {
        skins = GetPrefSkins();
        return skins;
    }
    public void SaveLocalToPrefs()
    {
        foreach (PlayerPrefSkins i in skins)
        {
            PlayerPrefs.SetInt(i.name, i.value);
        }
    }
    public PlayerPrefSkins[] GetPrefSkins()
    {
        PlayerPrefSkins[] skinsToReturn;
        if (PlayerPrefs.GetInt("prefsCreated").Equals(1))
        {
            skinsToReturn = new PlayerPrefSkins[]
            {
                new PlayerPrefSkins("default", PlayerPrefs.GetInt("default")), new PlayerPrefSkins("red",PlayerPrefs.GetInt("red")), new PlayerPrefSkins("pink", PlayerPrefs.GetInt("pink")),
                new PlayerPrefSkins("black", PlayerPrefs.GetInt("black")), new PlayerPrefSkins("glass", PlayerPrefs.GetInt("glass")), new PlayerPrefSkins("gold", PlayerPrefs.GetInt("gold")), new PlayerPrefSkins("reflex", PlayerPrefs.GetInt("reflex")),
            };
        }
        else
        {
            skinsToReturn = new PlayerPrefSkins[]
            {
                new PlayerPrefSkins("default", 2), new PlayerPrefSkins("red", 0), new PlayerPrefSkins("pink", 0),
                new PlayerPrefSkins("black", 0), new PlayerPrefSkins("glass", 0), new PlayerPrefSkins("gold", 0), new PlayerPrefSkins("reflex", 0),
            };
            PlayerPrefs.SetInt("prefsCreated", 1);
        }
        return skinsToReturn;
    }

    public int GetPrefCoins()
    {
        int coinsToReturn = 0;
        if (PlayerPrefs.GetInt("coins") > 0)
        {
            coinsToReturn = PlayerPrefs.GetInt("coins");
        }
        return coinsToReturn;
    }

    public void PurchaseSkin(int index)
    {
        SetValue(index, 1);
        int cost;/*
        switch (index)
        {
            case 1:
                cost = 175;
                break;
            case 2:
                cost = 250;
                break;
            case 3:
                cost = 350;
                break;
            case 4:
                cost = 500;
                break;
            case 5:
                cost = 750;
                break;
            case 6:
                cost = 1000;
                break;
            default:
                cost = 100;
                break;
        }
        if(coinsCollected > cost)
        {
            coinsCollected -= cost;
            PlayerPrefs.SetInt("coins", coinsCollected);
            coins.text = coinsCollected.ToString();
            SetValue(index, 1);
        }
        else
        {
            StartCoroutine(ShowAndHideError());
        }*/
    }

    IEnumerator ShowAndHideError()
    {
        error.SetActive(true);
        yield return new WaitForSeconds(2f);
        error.SetActive(false);
    }

    public void SelectSkin(int index)
    {
        SetValue(index, 2);
    }

    public void CollectCoin()
    {
        PlayerPrefs.SetInt("coins", coinsCollected++);
    }

    public void SpendCoins(int coins)
    {
        PlayerPrefs.SetInt("coins", coinsCollected-coins);
    }
}
public class PlayerPrefSkins
{
    public PlayerPrefSkins(string name, int value)
    {
        this.name = name;
        this.value = value;
    }
    public string name;
    public int value;
}

