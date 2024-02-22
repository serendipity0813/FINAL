using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ChoiceUIController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_gameNameText;
    [SerializeField] private TextMeshProUGUI m_gameDescriptionText;
    [SerializeField] private TextMeshProUGUI m_HighScoreText;
    [SerializeField] private TextMeshProUGUI m_MyScoreText;
    [SerializeField] private GameObject m_cardBack;
    [SerializeField] private GameObject[] m_cardBackImage;

    
    private void Start()
    {
        Invoke("CardFlip", 0.3f);
        m_cardBackImage[Random.Range(0, 84)].SetActive(true);
        EffectSoundManager.Instance.PlayEffect(38);

        int gameNumber = MiniGameManager.Instance.GameNumber;
        if(gameNumber == 0)
        {
            m_gameNameText.text = "랜덤게임 모드";
            m_gameDescriptionText.text = "랜덤으로 게임을 즐기는 모드입니다.";
            m_HighScoreText.text = PlayerDataManager.instance.m_playerData.rankingPoint[gameNumber].ToString();
            m_MyScoreText.text = PlayerDataManager.instance.m_playerData.rankingPoint[gameNumber].ToString();
        }
        else
        {
            m_gameNameText.text = MiniGameManager.Instance.MiniGames.games[gameNumber].gameName;
            m_gameDescriptionText.text = MiniGameManager.Instance.MiniGames.games[gameNumber].gameDescription;
            m_HighScoreText.text = PlayerDataManager.instance.m_playerData.rankingPoint[gameNumber].ToString();
            m_MyScoreText.text = PlayerDataManager.instance.m_playerData.rankingPoint[gameNumber].ToString();
        }

    }

    private void CardFlip()
    {
        m_cardBack.SetActive(false);
    }

}
