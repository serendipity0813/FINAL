using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UpDownRopeGame : MiniGameSetting
{
    [HideInInspector]public int clearCount;
    [HideInInspector]public int difficulty;
    [HideInInspector]public float timer;
    [SerializeField]private GameObject m_player;
    [SerializeField]private GameObject m_obstacle;
    private Vector3 m_playerPosition;
    private float m_positiony;

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
        //카메라 설정
        CameraManager.Instance.ChangeCamera(CameraView.Angle30View);
        CameraManager.Instance.SetFollowSpeed(10.0f);
        CameraManager.Instance.SetFollowTarget(m_player);
        CameraManager.Instance.ToggleCameraFollow();

        //인게임 text내용 설정 + 게임 승리조건
        clearCount = 1 + ((m_difficulty1 + m_difficulty2)/ 3);
        m_missionText.text = "장애물을 피해 바닥까지 내려가자!";
        difficulty = m_difficulty1;

        //맵의 위치값과 변동을 줄 y값 받아오기
        m_playerPosition = m_player.transform.position;
        m_positiony = m_playerPosition.y;

        for(int i=1; i <= m_difficulty2 + m_difficulty1 - 1; i++)
        {
             Instantiate(m_obstacle, m_obstacle.transform.position, Quaternion.identity, transform);
        }

    }

    private void FixedUpdate()
    {
        //마우스를 클릭할 때 마우스 위치를 받아온 후 위쪽 클릭중이면 올라가고 아래쪽 클릭중이면 내려가도록 함
        if (Input.GetMouseButton(0) && timer > 2)
        {
            float direction = Input.mousePosition.y - ((float)Screen.height / 2);

            if (m_positiony > -33 && direction < 0)
                m_positiony -= Time.deltaTime * 7;

            if (m_positiony < -1 && direction > 0)
                m_positiony += Time.deltaTime * 7;
        }


        m_player.transform.position = new Vector3(m_playerPosition.x, m_positiony, m_playerPosition.z);

    }

    private void Update()
    {
      
        //시간과 카운트 반영되는 코드
        m_timeText.text = (17- timer).ToString("0.00");
        m_countText.text = clearCount.ToString();

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        timer += Time.deltaTime;
        if (timer > 0.5 && m_missionPrefab.activeSelf == false)
            m_missionPrefab.SetActive(true);
        if (timer > 1.5 && m_missionPrefab.activeSelf == true)
            m_missionPrefab.SetActive(false);

        //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
        if (timer > 2)
        {
            m_timePrefab.SetActive(true);
            m_countPrefab.SetActive(true);
        }

        //게임 승리조건
        if (m_positiony < -32 && clearCount > 0)
        {
            m_clearPrefab.SetActive(true);
            timer = 10;
            Invoke("GameClear", 1);
            CameraManager.Instance.ToggleCameraFollow();
        }

        //게임 패배조건
        if (timer > 17 || clearCount <= 0)
        {
            m_failPrefab.SetActive(true);
            Invoke("GameFail", 1);
            CameraManager.Instance.ToggleCameraFollow();
        }


    }


}
