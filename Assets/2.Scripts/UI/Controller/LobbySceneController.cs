using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbySceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerNameText;
    [SerializeField] private TextMeshProUGUI m_playerLevelText;
    [SerializeField] private TextMeshProUGUI m_playerCoinText;
    [SerializeField] private TextMeshProUGUI m_playerDiamondText;

    private void Awake()
    {
        m_playerNameText.text = "Master";
        m_playerLevelText.text = "13";
        m_playerCoinText.text = "9999";
        m_playerDiamondText.text = "999";

        //m_playerNameText.text = PlayerDataManager.instance.m_playerData.name;
        //m_playerCoinText.text = PlayerDataManager.instance.m_playerData.coin.ToString();
    }

}
