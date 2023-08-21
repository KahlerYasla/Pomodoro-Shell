using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

// singleton class
public class WatchAndHarvestCredits : MonoBehaviour
{
    public static WatchAndHarvestCredits Instance { get; private set; }
    public TMPro.TextMeshProUGUI creditsText, harvestableCreditsText;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
    }

    public void WatchAndHarvestCreditsButtonClicked()
    {
        LoadRewardedAd(true);
    }

    #region AdMob Rewarded Ad -------------------------------------------------------------------------------------------------------

#if UNITY_ANDROID
    private readonly string _adUnitId = "ca-app-pub-9145901369649865/3215180761";
#elif UNITY_IPHONE
            private readonly string _adUnitId = "ca-app-pub-9145901369649865/3215180761";
#else
            private readonly string _adUnitId = "unused";
#endif

    private RewardedAd _rewardedAd;

    public void ShowRewardedAd()
    {
        Debug.LogWarning("ShowRewardedAd() called.");

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            Debug.LogWarning("ShowRewardedAd() called. _rewardedAd != null && _rewardedAd.CanShowAd()");
            _rewardedAd.Show(async (Reward reward) =>
            {
                Debug.LogWarning("ShowRewardedAd() called. _rewardedAd.Show(async (Reward reward) =>");
                // Reward the user by increasing credits by harvestable credits and save to the database
                DatabaseManager databaseManager = DatabaseManager.Instance;

                ProfileModel profile = await databaseManager.GetProfileModelAsync();

                profile.Credits += LocalSavedDataUtility.HarvestableCredits;
                databaseManager.UpdateDatabase(profile);

                // reset the harvestable credits
                LocalSavedDataUtility.HarvestableCredits = 0;

                // update the texts
                creditsText.text = profile.Credits.ToString();
                harvestableCreditsText.text = LocalSavedDataUtility.HarvestableCredits.ToString();
            });
        }
        else if (_rewardedAd == null)
        {
            Debug.LogWarning("ShowRewardedAd() called. _rewardedAd == null");
        }
        else if (!_rewardedAd.CanShowAd())
        {
            Debug.LogWarning("ShowRewardedAd() called. !_rewardedAd.CanShowAd()");
        }
    }

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd(bool showAdInstantly = false)
    {
        // // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        AdRequest adRequest = new();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                        + ad.GetResponseInfo());

                _rewardedAd = ad;

                if (showAdInstantly)
                    ShowRewardedAd();
            });
    }
    #endregion AdMob Rewarded Ad -------------------------------------------------------------------------------------------------------
}
