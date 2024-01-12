using UnityEngine;

public class RayController : MonoBehaviour
{
    [SerializeField] private int m_bulletCount;
    [SerializeField] private int m_winCount;
    [SerializeField] private int m_win;
    void Awake()
    {
        m_bulletCount = 5; // 처음에 주어지는 총알 개수
        m_winCount = 0; // 맞췄을 시 증가하는 카운터
        m_win = 3; // 미니 게임에 승리하기 위한 변수
    }
    void Update()
    {
        Bullet(); // 보기 편하게 만들기 위해 함수로 변경
    }

    void Bullet() // 업데이트에서 동작
    {
        if (m_bulletCount != 0) // 현재 총알이 0개가 아닐경우
        {
            if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼 클릭 시
            {
                m_bulletCount--; // 총알 개수 -
                HitFood();  // 레이 발사 함수 호출
                CheckWinLose(); // 승리 패배 판단 함수 호출
            }
        }
    }

    void HitFood() // 레이를 쏴서 푸드를 맞췄을 시
    {
        // 마우스 위치에서 레이 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit; // 레이캐스트 히트 정보 받아오기

        if (Physics.Raycast(ray, out hit))  // 레이캐스트 실행
        {
            GameObject hitObject = hit.collider.gameObject; // 맞은 오브젝트를 가져옴
            m_winCount++; // 승리 카운터 증가
            Destroy(hitObject); // 맞은 오브젝트를 삭제
        }
    }

    void CheckWinLose() // 승리 패배 판단 함수
    {
        if (m_winCount >= m_win)
        {
            // 승리시 로직
            m_bulletCount = 0; // 승리했을 시 총알 개수를 0개로 변경
            Debug.Log("이겼다!");
        }
        else if (m_winCount < m_win && m_bulletCount == 0)
        {
            // 패배시 로직
            Debug.Log("졌다!");
        }
    }
}
