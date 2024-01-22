using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerKingGame : MiniGameSetting
{
    private int m_stage = 1;    //현재는 임시로 숫자 1 사용
    private float m_timer;
    private int m_count;
    private bool m_clear;
    private int m_burgerLength;
    private int[] m_missionBurger;
    private int[] m_playerBurger;
    private Vector3 m_spawnPosition;
    public GameObject Bread;
    public GameObject Patty;
    public GameObject Vegetable;
    public GameObject Cheeze;
    


    protected override void Awake()
    {
        /*부모클래스 필드 및 메소드 받아오기
         인게임 UI 목록 : m_missionUI, m_timeUI, m_countUI, m_clearUI, m_failUI
         인게임 Text 목록 : m_missionText, m_timeText[0], m_timeText[1], m_countText[0], m_countText[1]
         배열의 경우 0번은 내용(남은시간, 남은횟수 등), 1번은 숫자 입니다.
         인게임 Method 목록 : GameClear(); , GameFail(); */

        base.Awake();
    }

    private void Start()
    {

        //인게임 text내용 설정 + 게임 승리조건
        m_burgerLength = m_stage + 5;
        m_clear = false;
        m_count = 0;
        m_missionText.text = "Make Perfact Burger";
        m_timeText[0].text = "TimeLimit";

        //햄버거 재료 스폰위치 설정
        m_spawnPosition = transform.position;
        m_spawnPosition.y += 10;

        //만들어야 하는 햄버거 랜덤으로 설정
        m_missionBurger = new int[m_burgerLength];
        m_playerBurger = new int[m_burgerLength];

        for (int i=0; i<m_burgerLength; i++)
        {
            if (i == 0)
                m_missionBurger[i] = 0;
            else if (i == m_burgerLength - 1)
                m_missionBurger[i] = 0;
            else
                m_missionBurger[i] = Random.Range(0, 4);
        }


    }

    private void Update()
    {
        //시간과 카운트 반영되는 코드
        m_timeText[1].text = (m_timer-2).ToString("0.00");

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        m_timer += Time.deltaTime;
        if (m_timer > 0.5 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (m_timer > 1.5 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (m_timer > 2)
        {
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }

        //게임 승리조건
        if (m_burgerLength == m_count)
        {
            ClearCheck();
            if(m_clear)
            {
                m_clearPrefab.SetActive(true);
                Invoke("GameClear", 1);
            }
            else
            {
                m_failPrefab.SetActive(true);
                Invoke("GameFail", 1);
            }
        }

        //게임 패배조건
        if (m_timer > 12 || m_count > m_burgerLength)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
        }


    }

    private void ClearCheck()
    {
        m_clear = true;
        for (int i=0; i<m_burgerLength; i++)
        {
            if (m_playerBurger[i] != m_missionBurger[i])
                m_clear = false;
        }
    }

    public void BreadBtn()
    {
        m_playerBurger[m_count] = 0;
        m_count++;
        Instantiate(Bread, m_spawnPosition, Quaternion.identity, transform);
    }

    public void PattyBtn()
    {
        m_playerBurger[m_count] = 1;
        m_count++;
        Instantiate(Patty, m_spawnPosition, Quaternion.identity, transform);
    }

    public void VegetableBtn()
    {
        m_playerBurger[m_count] = 2;
        m_count++;
        Instantiate(Vegetable, m_spawnPosition, Quaternion.identity, transform);
    }

    public void CheezeBtn()
    {
        m_playerBurger[m_count] = 3;
        m_count++;
        Instantiate(Cheeze, m_spawnPosition, Quaternion.identity, transform);
    }

}
