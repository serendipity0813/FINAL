using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbySceneController : ButtonHandler
{
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
        m_playerCoinText.text = PlayerDataManager.instance.m_playerData.coin.ToString();
        m_playerLevelText.text = PlayerDataManager.instance.m_playerData.level.ToString();
    }

}
