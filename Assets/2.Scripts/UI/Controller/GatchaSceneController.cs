using GoogleMobileAds.Api;
using TMPro;
using UnityEngine;

public class GatchaSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerMoneyText;
    [SerializeField] private GatchaMiniGame m_GatchaSimulator;
    [SerializeField] private GameObject m_spawnEffect;

    protected string m_adUnitId;
    protected RewardedAd m_rewardAd;

    private void Start()
    {
#if UNITY_ANDROID
        m_adUnitId = "ca-app-pub-3940256099942544/1044960115";//테스트 ID 
#else
        m_adUnitId = "unexpected_platform";
#endif
    }

    private void Update()
    {
        m_playerMoneyText.text = PlayerDataManager.instance.ChangeNumber(PlayerDataManager.instance.m_playerData.coin.ToString());
    }

    public void AdClick()
    {
        EffectSoundManager.Instance.PlayEffect(2);
        LoadAd();


        if (m_rewardAd.CanShowAd())
        {
            m_rewardAd.Show(GetReward);
        }
        else
        {
            Debug.Log("광고 재생 실패");
        }
    }

    public void SpawnEffect()
    {
        EffectSoundManager.Instance.PlayEffect(35);
        m_spawnEffect.SetActive(true);
    }

    //광고 시청 후 보상을 적용하는 함수
    private void GetReward(Reward reward)
    {
        Debug.Log("코인 100개 얻기");
    }

    //광고를 새로 받아오는 함수
    private void LoadAd()
    {

        //광고를 한번 시청하고 나면 RewardedAd 변수가 null로 바뀜, 따라 새로 시청할 때 마다 초기화 해주는 기능이 필요함
        RewardedAd.Load(m_adUnitId, new AdRequest.Builder().Build(), LoadCallback);

    }

    private void LoadCallback(RewardedAd rewardedAd, LoadAdError loadAdError)
    {
        if (rewardedAd != null)
        {
            m_rewardAd = rewardedAd;
            m_GatchaSimulator.GatchaWithOutCoin();//뽑기 시뮬레이터 실행
            Debug.Log("로드성공");
        }
        else
        {
            Debug.Log(loadAdError.GetMessage());
        }

    }
}
