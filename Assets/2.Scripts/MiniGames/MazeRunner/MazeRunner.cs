using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MazeRunner : MonoBehaviour
{

    [SerializeField] private DragToMoveController m_controller;
    [SerializeField] private GameObject m_wall;//미로 안쪽 벽의 프리펩

    private bool[,] m_map;
    private int m_mazeWidth = 11;//벽까지 포함한 크기
    private int m_mazeHeight = 17;//벽까지 포함한 크기
    private Vector2Int m_pos = Vector2Int.zero;
    private Vector2Int[] m_direction = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    private Stack<Vector2Int> m_stackDir = new Stack<Vector2Int>(); //지나온 길의 방향 저장
    private float m_timer = 0.0f;//타이머
    
    private void Start()
    {
        m_timer = 10.0f;
        m_controller.SetMoveSpeed(6.0f);

        m_map = new bool[m_mazeWidth, m_mazeHeight];//미로 크기 9x9
        m_pos = new Vector2Int(1, 1);

       

        for (int x = 0; x < m_mazeWidth; x++)
        {
            for (int y = 0; y < m_mazeHeight; y++)
            {
                m_map[x, y] = true; //모든 타일을 벽으로 채움
            }
        }

        BuildMaze();
        m_map[8, 15] = false; //출구 뚫어놓기
        //벽 배치
        for (int x = 1; x < m_mazeWidth-1; x++)
        {
            for (int y = 1; y < m_mazeHeight-1; y++)
            {
                if (m_map[x, y])
                {
                    GameObject obj = Instantiate(m_wall, transform.GetChild(1));
                    obj.transform.position = new Vector3(0.8f * x-4, 0.8f, 0.8f * y-6.8f);
                }
                
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        m_timer -= Time.deltaTime;
        m_controller.UpdateMove();
    }

    private void BuildMaze()
    {
        do
        {
            int index = -1; //-1은 갈 수 있는 길이 없음을 의미

            RandDirection(); //방향 무작위로 섞음

            for (int i = 0; i < m_direction.Length; i++)
            {
                if (CheckWall(i))
                {
                    index = i; //선택한 방향에 길이 없을 경우 방향 배열의 인덱스 저장
                    break;
                }
            }

            if (index != -1) //갈 수 있는 길이 있을 경우
            {
                for (int i = 0; i < 2; i++) //길과 길 사이에 벽을 생성하기 위해 3칸을 길로 바꿈
                {
                    m_stackDir.Push(m_direction[index]); //스택에 방향 저장
                    m_pos += m_direction[index]; //위치 변수 수정
                    m_map[m_pos.x, m_pos.y] = false; //타일 생성
                }
            }
            else //갈 수 있는 길이 없을 경우
            {
                for (int i = 0; i < 2; i++) //길을 만들 때 3칸을 만들었기 때문에 뒤로 돌아갈 때도 3칸 이동
                {
                    m_map[m_pos.x, m_pos.y] = false; //완성된 길 의미
                    m_pos += m_stackDir.Pop() * -1; //방향을 저장하는 스택에서 데이터를 꺼낸뒤 -1을 곱해 방향 반전
                }
            }
        }
        while (m_stackDir.Count != 0); //스택이 0이라는 것은 모든 길을 순회했다는 것을 의미하므로 반복문 종료


    }

    private void RandDirection() //무작위로 방향을 섞는 메소드
    {
        for (int i = 0; i < m_direction.Length; i++)
        {
            int randNum = Random.Range(0, m_direction.Length); //4방향 중 무작위로 방향 선택
            Vector2Int temp = m_direction[randNum]; //현재 인덱스에 해당하는 방향과 랜덤으로 선택한 방향을 서로 바꿈
            m_direction[randNum] = m_direction[i];
            m_direction[i] = temp;
        }
    }

    private bool CheckWall(int index)
    {
        if ((m_pos + m_direction[index] * 2).x > m_mazeWidth - 2) return false; //2를 곱하는 이유는 길과 길 사이에 벽이 있기 때문
        if ((m_pos + m_direction[index] * 2).x < 0) return false;
        if ((m_pos + m_direction[index] * 2).y > m_mazeHeight - 2) return false;
        if ((m_pos + m_direction[index] * 2).y < 0) return false;
        if (!m_map[(m_pos + m_direction[index] * 2).x, (m_pos + m_direction[index] * 2).y]) return false;

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Debug.LogFormat("GameClear in {0}sec", m_timer);
    }
}
