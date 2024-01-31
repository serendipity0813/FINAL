using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameChangeSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_stageNumber;

    private void Start()
    {
        m_stageNumber.text = PlayerDataManager.instance.m_playerData.stage.ToString();

        if (MiniGameManager.Instance.GameNumber == -1)
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
