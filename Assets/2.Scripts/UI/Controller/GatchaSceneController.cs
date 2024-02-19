using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatchaSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerMoneyText;
    [SerializeField] private GameObject m_spawnEffect;

    private void Start()
    {
        EffectSoundManager.Instance.PlayEffect(35);
    }
    private void Update()
    {
        m_playerMoneyText.text = PlayerDataManager.instance.m_playerData.coin.ToString();
    }

    public void AdClick()
    {
        Debug.Log("광고보기");
    }

    public void SpawnEffect()
    {
        m_spawnEffect.SetActive(true);
    }



}
