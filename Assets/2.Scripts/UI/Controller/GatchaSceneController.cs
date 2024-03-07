using GoogleMobileAds.Api;
using System;
using TMPro;
using UnityEngine;

public class GatchaSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerMoneyText;
    [SerializeField] private GatchaMiniGame m_GatchaSimulator;
    [SerializeField] private GameObject m_spawnEffect;

    private RewardedAd m_rewardAd;
    private AdRequest m_adRequest;
    private InterstitialAd m_interstitialAd;

#if UNITY_ANDROID
    private string m_adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
  private string m_adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string m_adUnitId = "unused";
#endif

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
    }

    private void Update()
    {
        m_playerMoneyText.text = PlayerDataManager.instance.ChangeNumber(PlayerDataManager.instance.m_playerData.coin.ToString());
    }

    public void AdClick()
    {
        EffectSoundManager.Instance.PlayEffect(2);

        LoadAd();
        ShowAd();
    }

    public void SpawnEffect()
    {
        EffectSoundManager.Instance.PlayEffect(35);
        m_spawnEffect.SetActive(true);
    }

    ////광고 시청 후 보상을 적용하는 함수
    //private void GetReward(Reward reward)
    //{
    //    //Debug.Log("무료 뽑기 획득");
    //    m_GatchaSimulator.GatchaWithOutCoin();
    //}

    //광고를 새로 받아오는 함수
    private void LoadAd()
    {
        // Load an interstitial ad
        InterstitialAd.Load(m_adUnitId, new AdRequest(),
            (InterstitialAd ad, LoadAdError loadAdError) =>
            {
                if (loadAdError != null)
                {
                    //Debug.Log("Interstitial ad failed to load with error: " +loadAdError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    //Debug.Log("Interstitial ad failed to load.");
                    return;
                }

                //Debug.Log("Interstitial ad loaded.");
                m_interstitialAd = ad;
            });

        //// Load a rewarded ad
        //RewardedAd.Load(m_adUnitId, new AdRequest(),
        //    (RewardedAd ad, LoadAdError loadError) =>
        //    {
        //        if (loadError != null)
        //        {
        //            //Debug.Log("Rewarded ad failed to load with error: " +loadError.GetMessage());
        //            return;
        //        }
        //        else if (ad == null)
        //        {
        //            //Debug.Log("Rewarded ad failed to load.");
        //            return;
        //        }

        //        //Debug.Log("Rewarded ad loaded.");
        //        m_rewardAd = ad;
        //    });
    }

    //받아온 광고를 보여주는 함수
    public void ShowAd()
    {

        if (m_interstitialAd != null && m_interstitialAd.CanShowAd())
        {
            m_interstitialAd.Show();
            m_GatchaSimulator.GatchaWithOutCoin();
        }
        else
        {
            //Debug.Log("Interstitial ad cannot be shown.");
        }

    }

}
