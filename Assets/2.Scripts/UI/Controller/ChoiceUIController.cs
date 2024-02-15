using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceUIController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_gameNameText;
    [SerializeField] private TextMeshProUGUI m_gameDescriptionText;
    [SerializeField] private TextMeshProUGUI m_HighScoreText;
    [SerializeField] private TextMeshProUGUI m_MyScoreText;

    //클릭한 게임에 대한 실시간 정보를 출력하도록 코드 필요
    private void Start()
    {
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


}
