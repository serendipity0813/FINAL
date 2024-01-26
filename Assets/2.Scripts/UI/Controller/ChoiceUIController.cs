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
        if(gameNumber == -1)
        {
            m_gameNameText.text = "Random Game Mode";
            m_gameDescriptionText.text = "Random Game Description";
            m_HighScoreText.text = "HighScore";
            m_MyScoreText.text = "MyScore";
        }
        else
        {
            m_gameNameText.text = MiniGameManager.Instance.MiniGames.games[gameNumber].gameName;
            m_gameDescriptionText.text = MiniGameManager.Instance.MiniGames.games[gameNumber].gameDescription;
            m_HighScoreText.text = "HighScore";
            m_MyScoreText.text = "MyScore";
        }

    }


}
