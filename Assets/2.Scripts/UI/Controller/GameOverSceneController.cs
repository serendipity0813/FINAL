using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_moneyText;
    [SerializeField] private TextMeshProUGUI m_EXPText;
    [SerializeField] private TextMeshProUGUI m_pointText;
    [SerializeField] private TextMeshProUGUI m_stageText;

    private int m_stageNum;
    private int m_moneyNum;
    private int m_ExpNum;
    private int m_pointNum;

    private void Start()
    {
        m_stageNum = MiniGameManager.Instance.Stage;
        m_stageNum = MiniGameManager.Instance.Money;
        m_stageNum = MiniGameManager.Instance.EXP;
        m_stageNum = MiniGameManager.Instance.Point;
        m_moneyText.text = m_moneyNum.ToString();
        m_EXPText.text = m_ExpNum.ToString();
        m_pointText.text = m_pointNum.ToString();
        m_stageText.text = m_stageNum.ToString();
    }
}
