using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameChangeSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_stageNumber;
    [SerializeField] private GameObject m_playerLife;

    private void Start()
    {
        m_stageNumber.text = "9999";

        if (MiniGameManager.Instance.GameName == "Random")
            Invoke("StartRandomGame", 1);
        else
            Invoke("StartChoiceGame", 1);

    }

    private void StartRandomGame()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.InGameScene);
        MiniGameManager.Instance.RandomGameStart();
    }

    private void StartChoiceGame()
    {
        GameSceneManager.Instance.SceneSelect(SCENES.InGameScene);
        MiniGameManager.Instance.ChoiceGameStart();
    }

}
