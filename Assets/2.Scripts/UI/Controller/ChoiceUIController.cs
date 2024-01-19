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
        m_gameNameText.text = MiniGameManager.Instance.GameName;
        m_gameDescriptionText.text = "Please Give me Description";
        m_HighScoreText.text = "HighScore";
        m_MyScoreText.text = "MyScore";
    }


}
