using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefMovement : MonoBehaviour
{
    private findthiefgame findThiefGame;
    private Vector3[] m_hidePosition;   //도둑이 숨을 동굴들의 위치값 배열
    private Vector3 m_targetPositon;        //다음 이동 목표
    private float m_thiefSpeed;
    private int m_length;
    private bool m_isGaming;
    private bool m_isChanging;
    private bool m_isMoving;

    private void Awake()
    {
        //findThiefGame 참조
        findThiefGame = transform.parent.GetComponent<findthiefgame>();
    }
    private void Start()
    {

        m_thiefSpeed = (PlayerDataManager.instance.m_playerData.stage / 3 + 1) * 0.04f + 0.1f;
        m_length = findThiefGame.m_hidePosition.Length;
        m_hidePosition = new Vector3[m_length];

        //ThiefCavaManager 로 부터 동굴의 위치값을 받아온 후 캐싱해두기
        for (int i = 0; i < m_length; i++)
        {
            m_hidePosition[i] = findThiefGame.m_hidePosition[i];
        }

        m_targetPositon = m_hidePosition[Random.Range(0, m_hidePosition.Length)];
    }

    private void FixedUpdate()
    {
        StepSound();

        // ThiefCavaManager 가 게임 및 위치이동 등을 조절하는 경우를 즉시 받아오기 위함
        m_isGaming = findThiefGame.IsGaiming;
        m_isChanging = findThiefGame.IsChanging;

        if (m_isGaming == true)
        {
            //현재 오브젝트의 위치가 다음 포지션과 다를 경우 해당 포지션으로 이동하도록 함
            if (gameObject.transform.position != m_targetPositon)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPositon, m_thiefSpeed);
            }
            //만약 같을 경우 다음 포지션을 찾아야하는지 체크 후 랜덤한 다음 포지션을 설정
            else
            {
                if (m_isChanging)
                {
                    m_isMoving = true;
                    m_targetPositon = m_hidePosition[Random.Range(0, m_hidePosition.Length)];
                }
                  
            }
        }


    }

    private void StepSound()
    {
        if(m_isMoving)
        {
            m_isMoving = !m_isMoving;
            EffectSoundManager.Instance.PlayEffect(20);
        }
    
    }
}
