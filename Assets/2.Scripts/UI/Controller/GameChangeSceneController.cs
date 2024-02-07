using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameChangeSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_stageNumber;
    [SerializeField] private GameObject[] m_life;
    [SerializeField] private GameObject[] m_loseLife;

    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.Angle30View);
        m_stageNumber.text = (PlayerDataManager.instance.m_playerData.stage+1).ToString();
        LifeCheck();

        if (MiniGameManager.Instance.GameNumber == -1)
            Invoke("StartRandomGame", 2);
        else
            Invoke("StartChoiceGame", 2);


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

    private void LifeCheck()
    {
        int lifenum = PlayerDataManager.instance.m_playerData.life;

        for (int i = 0; i < 4; i++)
        {
            if(lifenum > i)
                m_life[i].SetActive(true);
            else
                m_life[i].SetActive(false);

            if (i == lifenum && MiniGameManager.Instance.m_clearCheck == -1)
            {
                m_loseLife[i].SetActive(true);
            }

        }
    }

}
