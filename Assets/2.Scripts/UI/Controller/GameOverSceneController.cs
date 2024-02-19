using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_moneyText;
    [SerializeField] private TextMeshProUGUI m_EXPText;
    [SerializeField] private TextMeshProUGUI m_stagePointText;
    [SerializeField] private TextMeshProUGUI m_timePointText;
    [SerializeField] private TextMeshProUGUI m_bonusPointText;
    [SerializeField] private TextMeshProUGUI m_totalPointText;

    private int m_moneyNum;
    private int m_ExpNum;
    private int m_stagePointNum;
    private int m_timePointNum;
    private int m_bonusPointNum;
    private int m_totalPointNum;

    private void Start()
    {
        m_moneyNum = PlayerDataManager.instance.m_playerData.rewardCoin;
        m_ExpNum = PlayerDataManager.instance.m_playerData.rewardExp;
        m_stagePointNum = (PlayerDataManager.instance.m_playerData.stage+1) * 1000;
        m_timePointNum = PlayerDataManager.instance.m_playerData.timePoint;
        m_bonusPointNum = PlayerDataManager.instance.m_playerData.bonusPoint;
        m_totalPointNum = m_stagePointNum + m_timePointNum + m_bonusPointNum;

        m_moneyText.text = m_moneyNum.ToString();
        m_EXPText.text = m_ExpNum.ToString();
        m_stagePointText.text = m_stagePointNum.ToString();
        m_timePointText.text = m_timePointNum.ToString();
        m_bonusPointText.text = m_bonusPointNum.ToString();
        m_totalPointText.text = m_totalPointNum.ToString();

        MiniGameManager.Instance.GameReset();

        EffectSoundManager.Instance.PlayEffect(32);
    }
}
