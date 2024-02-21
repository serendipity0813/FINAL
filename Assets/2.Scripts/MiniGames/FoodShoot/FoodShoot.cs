using UnityEngine;
using UnityEngine.EventSystems;

public class FoodShoot : MiniGameSetting
{
    private int m_bulletCount; // 처음에 주어지는 총알 개수
    private int m_winCount = 0; // 승리 카운트 선언
    private int m_win; // 미니 게임에 승리하기 위한 변수
    private bool m_end = false; // 게임이 끝났는가?
    private Camera m_camera; // 레이를 위한 카메라
    public int m_repetition; // 과일을 몇번 뿌릴지
    private float m_timer; // 현재 시간
    private bool m_startTimer = false; // 첫 스타트 시간 체크


    protected override void Awake()
    {
        base.Awake();
        m_bulletCount = 5; //총알 5발 고정

        // 1-1, 2-1, 3-1 에 해당되는 난이도
        switch (m_difficulty1)
        {
            case 0:
            case 1:
                m_win = 3;
                break;
            case 2:
                m_win = 4;
                break;
            case 3:
                m_win = 5;
                break;
        }

        // 1-1, 1-2, 1-3 에 해당되는 난이도
        switch (m_difficulty2)
        {
            case 0:
            case 1:
                m_repetition = 10;
                break;
            case 2:
                m_repetition = 8;
                break;
            case 3:
                m_repetition = 6;
                break;
        }
        m_timer = 12f; // 12초 고정
    }
    private void Start()
    {
        //인게임 text내용 설정 + 게임 승리조건
        m_missionText.text = m_bulletCount + "발 안에 음식을 " + m_win + "개 맞춰라";

        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        m_camera = CameraManager.Instance.GetCamera();
    }
    void Update()
    {
        UiTime();
        Bullet(); // 보기 편하게 만들기 위해 함수로 변경
    }
    void UiTime()
    {
        //시간과 카운트 반영되는 코드
        m_timeText.text = m_timer.ToString("0.00");
        m_countText.text = m_winCount.ToString() + "/" + m_win.ToString();

        //게임 시작 후 미션을 보여주고 나서 1초 후 지움
        if (m_timer > 0f)
        {
            if (!m_end)
            {
                m_timer -= Time.deltaTime;
            }
        }
        if (!m_startTimer)
        {
            if (m_timer < 11.5f && m_missionPrefab.activeSelf == false)
                m_missionPrefab.SetActive(true);
            if (m_timer < 10.5f && m_missionPrefab.activeSelf == true)
                m_missionPrefab.SetActive(false);

            //2초 후 부터 실제 게임시작 - 시간제한과 클리어를 위한 카운트 ui를 출력
            if (m_timer < 10f)
            {
                m_timer = 10f;
                m_startTimer = true;
                m_timePrefab.SetActive(true);
                m_countPrefab.SetActive(true);
            }
        }
    }
    void Bullet() // 업데이트에서 동작
    {
        if (m_startTimer)
        {
            if (m_bulletCount != 0) // 현재 총알이 0개가 아닐경우
            {
                if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼 클릭 시
                {
                    HitFood();  // 레이 발사 함수 호출
                    CheckWinLose(); // 승리 패배 판단 함수 호출
                }
            }
        }
    }

    void HitFood() // 레이를 쏴서 푸드를 맞췄을 시
    {

        // 마우스 위치에서 레이 생성
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // 레이캐스트 히트 정보 받아오기

        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }
        if (Physics.Raycast(ray, out hit))  // 레이캐스트 실행
        {
            
            m_bulletCount--; // 총알 개수 -
            GameObject hitObject = hit.collider.gameObject; // 맞은 오브젝트를 가져옴
            m_winCount++; // 승리 카운터 증가
            EffectSoundManager.Instance.PlayEffect(1);
            Destroy(hitObject); // 맞은 오브젝트를 삭제
        }
        else
        {
            m_bulletCount--; // 총알 개수 -
            EffectSoundManager.Instance.PlayEffect(18);
        }

    }

    void CheckWinLose() // 승리 패배 판단 함수
    {
        if (!m_end)
        {
            if (m_winCount >= m_win)
            {
                // 승리시 로직
                m_bulletCount = 0; // 승리했을 시 총알 개수를 0개로 변경
                Debug.Log("이겼다!");
                m_clearPrefab.SetActive(true);
                m_end = true;
                Invoke("GameClear", 1);
            }
            if (m_winCount < m_win && (m_bulletCount == 0 || m_timer <= 0f))
            {
                // 패배시 로직
                Debug.Log("졌다!");
                m_failPrefab.SetActive(true);
                m_end = true;
                Invoke("GameFail", 1);
            }
        }
    }
}
