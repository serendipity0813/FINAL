using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameChangeSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_stageNumber;
    [SerializeField] private GameObject[] m_life;   //플레이어 라이프(하트) 이미지
    [SerializeField] private GameObject[] m_loseLife;   //땅으로 떨어지는 하트

    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.Angle30View);
        m_stageNumber.text = (PlayerDataManager.instance.m_playerData.stage+1).ToString();
        LifeCheck();

        //전환 애니메이션 적용을 위해 2초의 딜레이 설정
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

    //플레이어 라이프를 체크하여 이미지 및 애니메이션 동작
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
