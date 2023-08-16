using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class WatchAndHarvestCredits : MonoBehaviour
{
    public TMPro.TextMeshProUGUI creditsText, harvestableCreditsText;

#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-9145901369649865/3215180761";
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-9145901369649865/3215180761";
#else
    private string _adUnitId = "ca-app-pub-9145901369649865/3215180761";
#endif

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
    }

    public void WatchAndHarvestCreditsButtonClicked()
    {
        LoadRewardedAd();
    }

    #region AdMob Rewarded Ad -------------------------------------------------------------------------------------------------------

    private RewardedAd _rewardedAd;

    public void ShowRewardedAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show(async (Reward reward) =>
            {
                // Reward the user by increasing credits by harvestable credits and save to the database
                DatabaseManager databaseManager = new DatabaseManager();

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
    }

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

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


            });

        ShowRewardedAd();
    }
    #endregion AdMob Rewarded Ad -------------------------------------------------------------------------------------------------------
}
