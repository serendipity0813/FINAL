using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbySceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerNameText;
    [SerializeField] private TextMeshProUGUI m_playerCoinText;

    private void Awake()
    {
        m_playerNameText.text = "Master";
        m_playerCoinText.text = "9999";
        //m_playerNameText.text = PlayerDataManager.instance.m_playerData.name;
        //m_playerCoinText.text = PlayerDataManager.instance.m_playerData.coin.ToString();
    }

}
