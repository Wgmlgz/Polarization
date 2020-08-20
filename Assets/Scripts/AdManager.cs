using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace AdManager
{
    public class AdManager : MonoBehaviour
    {
        public bool b;

        private BannerView bannerView;
        private InterstitialAd interstitialAd;
        private RewardedAd rewardedAd;
        private float deltaTime;

        public UnityEvent OnAdLoadedEvent;
        public UnityEvent OnAdFailedToLoadEvent;
        public UnityEvent OnAdOpeningEvent;
        public UnityEvent OnAdFailedToShowEvent;
        public UnityEvent OnUserEarnedRewardEvent;
        public UnityEvent OnAdClosedEvent;
        public UnityEvent OnAdLeavingApplicationEvent;
        public bool showFpsMeter = true;
        public Text fpsMeter;
        public Text statusText;

        public static AdManager m_instance;

        #region UNITY MONOBEHAVIOR METHODS

        private void CreateInstance()
        {
            if (!m_instance)
            {
                m_instance = this;


                List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

                // Add some test device IDs (replace with your own device IDs).
                #if UNITY_IPHONE
                    deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
                #elif UNITY_ANDROID
                    deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
                #endif

                // Configure TagForChildDirectedTreatment and test device IDs.
                RequestConfiguration requestConfiguration =
                    new RequestConfiguration.Builder()
                    .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
                    .SetTestDeviceIds(deviceIds).build();

                MobileAds.SetRequestConfiguration(requestConfiguration);

                // Initialize the Google Mobile Ads SDK.
                MobileAds.Initialize(HandleInitCompleteAction);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                m_instance.statusText = statusText;
                m_instance.fpsMeter = fpsMeter;
                Destroy(this.gameObject);
            }
        }

        public void Start()
        {
            gameObject.transform.parent = null;
            showFpsMeter = (PlayerPrefs.GetInt("Fps") != 0);
            CreateInstance();
        }

        private void HandleInitCompleteAction(InitializationStatus initstatus)
        {
            // Callbacks from GoogleMobileAds are not guaranteed to be called on
            // main thread.
            // In this example we use MobileAdsEventExecutor to schedule these calls on
            // the next Update() loop.
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                statusText.text = "Initialization complete";
                RequestAndLoadInterstitialAd();
                RequestAndLoadRewardedAd();
            });
        }

        private void Update()
        {
            if (fpsMeter == null)
            {
                return;
            }
            if (showFpsMeter)
            {
                fpsMeter.gameObject.SetActive(true);
                deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
                float fps = 1.0f / deltaTime;
                fpsMeter.text = string.Format("{0:0.} fps", fps);
            }
            else
            {
                fpsMeter.gameObject.SetActive(false);
            }
        }

        #endregion

        #region HELPER METHODS

        private AdRequest CreateAdRequest()
        {
            return new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
                .AddKeyword("unity-admob-sample")
                .TagForChildDirectedTreatment(false)
                .AddExtra("color_bg", "9B30FF")
                .Build();
        }

        #endregion

        #region BANNER ADS

        public void RequestBannerAd()
        {
            statusText.text = "Requesting Banner Ad.";
            // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
            string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
            // Clean up banner before reusing
            if (bannerView != null)
            {
                bannerView.Destroy();
            }

            // Create a 320x50 banner at top of the screen
            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

            // Add Event Handlers
            bannerView.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
            bannerView.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
            bannerView.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
            bannerView.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();
            bannerView.OnAdLeavingApplication += (sender, args) => OnAdLeavingApplicationEvent.Invoke();

            // Load a banner ad
            bannerView.LoadAd(CreateAdRequest());
        }

        public void DestroyBannerAd()
        {
            if (bannerView != null)
            {
                bannerView.Destroy();
            }
        }

        #endregion

        #region INTERSTITIAL ADS

        public void RequestAndLoadInterstitialAd()
        {
            statusText.text = "Requesting Interstitial Ad.";
#if UNITY_EDITOR
            string adUnitId = "unused";
#elif UNITY_ANDROID
                string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
                string adUnitId = "unexpected_platform";
#endif
            adUnitId = "ca-app-pub-4800162937668095/7237597358";
            // Clean up interstitial before using it
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
            }

            interstitialAd = new InterstitialAd(adUnitId);

            // Add Event Handlers
            interstitialAd.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();
            interstitialAd.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
            interstitialAd.OnAdOpening += (sender, args) => OnAdOpeningEvent.Invoke();
            interstitialAd.OnAdClosed += (sender, args) => OnAdClosedEvent.Invoke();
            interstitialAd.OnAdLeavingApplication += (sender, args) => OnAdLeavingApplicationEvent.Invoke();

            // Load an interstitial ad
            interstitialAd.LoadAd(CreateAdRequest());
        }

        public void ShowInterstitialAd()
        {
            if (interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
            }
            else
            {
                RequestAndLoadInterstitialAd();
                statusText.text = "Interstitial ad is not ready yet";
            }
        }

        public void DestroyInterstitialAd()
        {
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
            }
        }
        #endregion

        #region REWARDED ADS

        public void RequestAndLoadRewardedAd()
        {
            statusText.text = "Requesting Rewarded Ad.";
#if UNITY_EDITOR
            string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif
            adUnitId = "ca-app-pub-4800162937668095/9023751507";
            // create new rewarded ad instance
            rewardedAd = new RewardedAd(adUnitId);

            // Add Event Handlers
            /*
            rewardedAd.OnAdLoaded += (sender, args) => 
            rewardedAd.OnAdFailedToLoad += (sender, args) => 
            rewardedAd.OnAdOpening += (sender, args) => 
            rewardedAd.OnAdFailedToShow += (sender, args) => 
            rewardedAd.OnAdClosed += (sender, args) => 
            rewardedAd.OnUserEarnedReward += (sender, args) => 
            */
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

            // Create empty ad request
            rewardedAd.LoadAd(CreateAdRequest());
        }

        public void ShowRewardedAd()
        {
            if (rewardedAd != null)
            {
                rewardedAd.Show();
                RequestAndLoadRewardedAd();
            }
            else
            {
                statusText.text = "Rewarded ad is not ready yet.";
            }
        }

        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
            OnAdLoadedEvent.Invoke();
        }
        public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToLoad event received with message: "
                                 + args.Message);
            OnAdFailedToLoadEvent.Invoke();
        }
        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdOpening event received");
            OnAdOpeningEvent.Invoke();
        }
        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: "
                                 + args.Message);
            OnAdFailedToShowEvent.Invoke();
        }
        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdClosed event received");
            OnAdClosedEvent.Invoke();
            RequestAndLoadRewardedAd();
        }
        public void HandleUserEarnedReward(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            b = true;
            Debug.Log(
                "HandleRewardedAdRewarded event received for "
                            + amount.ToString() + " " + type);
            Debug.Log(b);
            b = true;
        }

        #endregion
    }
}