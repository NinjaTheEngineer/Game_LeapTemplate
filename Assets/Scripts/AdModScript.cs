using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using GoogleMobileAds.Api;

public class AdModScript : MonoBehaviour
{
    public DeadScreenScript deadScript;
    public static AdModScript instance = null;

    public GameObject screensUi;

    string App_ID = "ca-app-pub-5279367620572875~9704028921";

    string Banner_Ad_Id = "ca-app-pub-3940256099942544/6300978111";
    string Interstitial_Ad_Id = "ca-app-pub-3940256099942544/1033173712";
    string Rewarded_Ad_Id = "ca-app-pub-3940256099942544/5224354917";

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    string DEVICE_ID = "L2N0219701003799 [MAR-LX1A]";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        //MobileAds.Initialize(App_ID);
        deadScript = FindObjectOfType<DeadScreenScript>().GetComponent<DeadScreenScript>();
        AdRequestInitializer();
    }

    public void AdRequestInitializer()
    {
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(DEVICE_ID)
            .Build();
    }
    #region Banner Ad

    public void LoadBanner()
    {
        this.bannerView = new BannerView(Banner_Ad_Id, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
    }

    public void ShowBannerAd()
    {
        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }

    #endregion

    #region Interstitial Ad

    public void LoadInterstitial()
    {
        this.interstitialAd = new InterstitialAd(Interstitial_Ad_Id);

        AdRequest request = new AdRequest.Builder().Build();

        // Called when an ad request has successfully loaded.
        this.interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialAd.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        this.interstitialAd.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (this.interstitialAd.IsLoaded())
        {
            this.interstitialAd.Show();
        }
    }



    #endregion

    #region Reward Ad

    public void LoadRewardedAd()
    {
        this.rewardedAd = new RewardedAd(Rewarded_Ad_Id);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();

        this.rewardedAd.LoadAd(request);
    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            Time.timeScale = 0f;
            this.rewardedAd.Show();
        }
    }
    #endregion

    #region Events

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        ShowInterstitial();

    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)

    {
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
    #endregion

    #region Rewarded Ads Events
    //Rewarded Ads Events
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        screensUi.SetActive(false);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        LoadRewardedAd();
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        deadScript.PlayerRestart();

        StartCoroutine(waitForOpenScreen());


        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }
    #endregion

    IEnumerator waitForOpenScreen()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        screensUi.SetActive(true);
    }
}
