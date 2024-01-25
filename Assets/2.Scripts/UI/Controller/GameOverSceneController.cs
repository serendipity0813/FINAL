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

    private void Start()
    {
        m_moneyText.text = "1234";
        m_EXPText.text = "5678";
        m_pointText.text = "4321";
        m_stageText.text = "13";
    }
}
