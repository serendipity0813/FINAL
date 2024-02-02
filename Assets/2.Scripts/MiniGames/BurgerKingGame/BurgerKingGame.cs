using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;

public class BurgerKingGame : MiniGameSetting
{
    private float m_timer;
    private int m_count;
    private int m_difficulty;
    private bool m_clear;
    private int m_burgerLength;
    private int[] m_missionBurger;
    private int[] m_playerBurger;
    private Vector3 m_burgerspawnPosition;
    private Vector3 m_missionSpawnPosition;
    public GameObject Bread;
    public GameObject Patty;
    public GameObject Vegetable;
    public GameObject Cheeze;
    public GameObject MissionTable;
    public GameObject MenuTable;


    protected override void Awake()
    {
        /*부모클래스 필드 및 메소드 받아오기
         인게임 UI 목록 : m_missionUI, m_timeUI, m_countUI, m_clearUI, m_failUI
         인게임 Text 목록 : m_missionText, m_timeText, m_timeText, m_countText, m_countText
         배열의 경우 0번은 내용(남은시간, 남은횟수 등), 1번은 숫자 입니다.
         인게임 Method 목록 : GameClear(); , GameFail(); */

        base.Awake();
    }

    private void Start()
    {
        //인게임 text내용 설정 + 게임 승리조건
        m_difficulty = m_difficulty1 * 3 + m_difficulty2 - 3;
        m_burgerLength = m_difficulty + 4;
        m_clear = false;
        m_count = 0;
        m_missionText.text = "똑같은 햄버거를 만들어보자!";

        //햄버거 재료 스폰위치 설정
        m_missionSpawnPosition = MissionTable.transform.position;
        m_burgerspawnPosition = MenuTable.transform.position;
        m_missionSpawnPosition.y++;
        m_burgerspawnPosition.y++;

        //만들어야 하는 햄버거 랜덤으로 설정
        m_missionBurger = new int[m_burgerLength];
        m_playerBurger = new int[m_burgerLength + 5];

        for (int i=0; i<m_burgerLength; i++)
        {
            //첫 번재 재료와 마지막 재료는 빵이 나오도록 하기
            if (i == 0)
                m_missionBurger[i] = 0;
            else if (i == m_burgerLength - 1)
                m_missionBurger[i] = 0;
            else
                m_missionBurger[i] = Random.Range(0, 4);
        }

        //미션버거 생성
        for(int i=0; i<m_burgerLength;i++)
        {
            //0~3 숫자에 따라서 재료를 생성하고 생성포인트의 y값을 조금씩 올리기
            if (m_missionBurger[i] == 0)
            {
                Instantiate(Bread, m_missionSpawnPosition, Quaternion.identity, transform);
                m_missionSpawnPosition.y += (float)0.5;
            }   
            else if(m_missionBurger[i] == 1)
            {
                Instantiate(Patty, m_missionSpawnPosition, Quaternion.identity, transform);
                m_missionSpawnPosition.y += (float)0.5;
            }
            else if (m_missionBurger[i] == 2)
            {
                Instantiate(Vegetable, m_missionSpawnPosition, Quaternion.identity, transform);
                m_missionSpawnPosition.y += (float)0.5;
            }
            else if (m_missionBurger[i] == 3)
            {
                Instantiate(Cheeze, m_missionSpawnPosition, Quaternion.identity, transform);
                m_missionSpawnPosition.y += (float)0.5;
            }
              
        }

    }

    private void Update()
    {
        //시간과 카운트 반영되는 코드
        m_timeText.text = (12 - m_timer).ToString("0.00");

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
        }

        //게임 승리조건
        if (m_burgerLength == m_count)
        {
            ClearCheck();
            if(m_clear)
            {
                m_clearPrefab.SetActive(true);
                Invoke("GameClear", 1);
                m_clear = false;
            }
            else if(!m_clear)
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
        //미션 버거와 만든 버거가 같은지 체크
        m_clear = true;
        for (int i=0; i<m_burgerLength; i++)
        {
            if (m_playerBurger[i] != m_missionBurger[i])
                m_clear = false;
        }
    }

    //버튼 입력에 따라 버거 재료를 만들어주는 함수들
    public void BreadBtn()
    {
        m_playerBurger[m_count] = 0;
        m_count++;
        m_burgerspawnPosition.y += (float)0.5;
        Instantiate(Bread, m_burgerspawnPosition, Quaternion.identity, transform);
    }

    public void PattyBtn()
    {
        m_playerBurger[m_count] = 1;
        m_count++;
        m_burgerspawnPosition.y += (float)0.5;
        Instantiate(Patty, m_burgerspawnPosition, Quaternion.identity, transform);
    }

    public void VegetableBtn()
    {
        m_playerBurger[m_count] = 2;
        m_count++;
        m_burgerspawnPosition.y += (float)0.5;
        Instantiate(Vegetable, m_burgerspawnPosition, Quaternion.identity, transform);
    }

    public void CheezeBtn()
    {
        m_playerBurger[m_count] = 3;
        m_count++;
        m_burgerspawnPosition.y += (float)0.5;
        Instantiate(Cheeze, m_burgerspawnPosition, Quaternion.identity, transform);
    }

}
