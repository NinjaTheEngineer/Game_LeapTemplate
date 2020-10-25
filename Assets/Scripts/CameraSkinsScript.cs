using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraSkinsScript : MonoBehaviour
{
    public AdModScript ads;

    private PlayerPrefManagerScript skinScript;
    public GameObject playerPref;
    public PlayerPrefSkins[] skins;

    public GameObject unlockedScreen;
    public GameObject lockedScreen;
    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject selectButton;
    public GameObject selectedText;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textPrice;

    public SceneFader sceneFader;

    private Vector3 mainPos = new Vector3(-0.2f, 3, -6);

    public float speed = 1f;

    public int currentIndex = 0;
    public int selectedIndex;
    public bool cameraMoving = false;
    public bool movingLeft = false;
    public bool movingRight = false;

    public Vector3 posToMove;
    void Start()
    {
        skinScript = playerPref.GetComponent<PlayerPrefManagerScript>();
        skins = skinScript.GetSkinsFromPrefs();
        foreach (PlayerPrefSkins i in skins)
        {
            if (i.value.Equals(2))
            {
                selectedIndex = currentIndex;
                StartPosition();
                UpdateNameAndPrice();
            }
            currentIndex++;
        }
        posToMove = transform.position;
    }
    private void Update()
    {
        if(cameraMoving) {
            transform.position = Vector3.Lerp(transform.position, posToMove, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, posToMove) < 0.25f && movingRight ||
                Vector3.Distance(posToMove, transform.position) < 0.25f && movingLeft)
            {
                CheckIfStatus();
                UpdateNameAndPrice();
                speed = 12;
                if (Vector3.Distance(transform.position, posToMove) < 0.010f && movingRight || 
                    Vector3.Distance(posToMove, transform.position) < 0.010f && movingLeft)
                {
                    transform.position = posToMove;
                    cameraMoving = false;
                    movingRight = false;
                    movingLeft = false;
                    speed = 4f;
                }
            }
        }
    }

    void StartPosition()
    {
        selectButton.SetActive(false);
        selectedText.SetActive(true);
        CheckIfStatus();
        switch (currentIndex)
        {
            case 1:
                transform.position = new Vector3(mainPos.x + 4, mainPos.y, mainPos.z);
                break;
            case 2:
                transform.position = new Vector3(mainPos.x + 4 * 2, mainPos.y, mainPos.z);
                break;
            case 3:
                transform.position = new Vector3(mainPos.x + 4 * 3, mainPos.y, mainPos.z);
                break;
            case 4:
                transform.position = new Vector3(mainPos.x + 4 * 4, mainPos.y, mainPos.z);
                break;
            case 5:
                transform.position = new Vector3(mainPos.x + 4 * 5, mainPos.y, mainPos.z);
                break;
            case 6:
                transform.position = new Vector3(mainPos.x + 4 * 6, mainPos.y, mainPos.z);
                nextButton.SetActive(false);
                break;
            default:
                transform.position = mainPos;
                previousButton.SetActive(false);
                break;
        }
    }

    public void PreviousButtonClick()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonTouch");
        if (!cameraMoving)
        {
            selectedIndex--;

            if (selectedIndex.Equals(5))
                nextButton.SetActive(true);
            if (selectedIndex.Equals(0))
                previousButton.SetActive(false);

            posToMove = new Vector3(transform.position.x - 4, transform.position.y, transform.position.z);
            cameraMoving = true;
            movingLeft = true;
        }
    }

    public void NextButtonClick()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonTouch");
        if (!cameraMoving)
        {
            selectedIndex++;

            if (selectedIndex.Equals(6))
                nextButton.SetActive(false);
            if (selectedIndex.Equals(1))
                previousButton.SetActive(true);

            posToMove = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z);
            cameraMoving = true;
            movingRight = true;
        }
    }

    public void CheckIfStatus()
    {
        skins = skinScript.GetSkinsFromPrefs();
        if (skins[selectedIndex].value.Equals(0))
        {
            lockedScreen.SetActive(true);
            unlockedScreen.SetActive(false);
        }else if (skins[selectedIndex].value.Equals(1))
        {
            lockedScreen.SetActive(false);
            unlockedScreen.SetActive(true);
            selectButton.SetActive(true);
            selectedText.SetActive(false);
        }
        else
        {
            lockedScreen.SetActive(false);
            unlockedScreen.SetActive(true);
            selectButton.SetActive(false);
            selectedText.SetActive(true);
        }
    }

    public void UpdateNameAndPrice()
    {
        switch (selectedIndex)
        {
            case 1:
                SetNameAndPrice("RED", 175);
                break;
            case 2:
                SetNameAndPrice("PINK", 250);
                break;
            case 3:
                SetNameAndPrice("BLACK", 350);
                break;
            case 4:
                SetNameAndPrice("GLASS", 500);
                break;
            case 5:
                SetNameAndPrice("GOLD", 750);
                break;
            case 6:
                SetNameAndPrice("REFLEX", 1000);
                break;
            default:
                SetNameAndPrice("DEFAULT", 100);
                break;
        }
    }

    private void SetNameAndPrice(string name, int price)
    {
        textName.text = name;
        textPrice.text = price.ToString();
    }

    public void OnBackClicked()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        sceneFader.FadeTo("InfinityLevel");
    }

    public void PurchaseItem()
    {
        FindObjectOfType<AudioManager>().PlaySound("SkinPurchase");
        skinScript.PurchaseSkin(selectedIndex);
        skins = skinScript.GetSkinsFromPrefs();
        CheckIfStatus();
        UpdateNameAndPrice();
    }

    public void SelectItem()
    {
        FindObjectOfType<AudioManager>().PlaySound("SkinSelected");
        skinScript.SelectSkin(selectedIndex);
        skins = skinScript.GetSkinsFromPrefs();
        CheckIfStatus();
        UpdateNameAndPrice();
    }
}
