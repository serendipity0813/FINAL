using TMPro;
using UnityEngine;

public class GatchaSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerMoneyText;
    [SerializeField] private GameObject m_spawnEffect;

    private void Update()
    {
        m_playerMoneyText.text = PlayerDataManager.instance.ChangeNumber(PlayerDataManager.instance.m_playerData.coin.ToString());
    }

    public void AdClick()
    {
        EffectSoundManager.Instance.PlayEffect(2);
        Debug.Log("광고보기");
    }

    public void SpawnEffect()
    {
        EffectSoundManager.Instance.PlayEffect(35);
        m_spawnEffect.SetActive(true);
    }
}
