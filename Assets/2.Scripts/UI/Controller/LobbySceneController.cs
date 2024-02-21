using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LobbySceneController : ButtonHandler
{
 
    [SerializeField] private GameObject[] m_profiles;
    [SerializeField] private TextMeshProUGUI m_playerNameText;
    [SerializeField] private TextMeshProUGUI m_playerLevelText;
    [SerializeField] private TextMeshProUGUI m_playerEXPText;
    [SerializeField] private TextMeshProUGUI m_playerCoinText;
    [SerializeField] private Slider m_expSlider;

    private void Awake()
    {
        m_expSlider.value = PlayerDataManager.instance.m_playerData.exp;
        m_playerNameText.text = PlayerDataManager.instance.m_playerData.name;
        m_playerEXPText.text = PlayerDataManager.instance.m_playerData.exp.ToString() + "%";
        m_playerLevelText.text = PlayerDataManager.instance.m_playerData.level.ToString();
        m_playerCoinText.text = PlayerDataManager.instance.m_playerData.coin.ToString();
        m_profiles[PlayerDataManager.instance.m_playerData.profileIndex].SetActive(true);

    }


    public void MarkChange()
    {
        int index = Random.Range(0, 84);
        m_profiles[PlayerDataManager.instance.m_playerData.profileIndex].SetActive(false);
        PlayerDataManager.instance.m_playerData.profileIndex = index;
        m_profiles[PlayerDataManager.instance.m_playerData.profileIndex].SetActive(true);
        EffectSoundManager.Instance.PlayEffect(19);
    }
}
