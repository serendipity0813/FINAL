using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    [SerializeField] private GameObject m_blueCar;

    private int[] m_cars;//배치된 차들
    private int m_difficulty = 1;//현재 난이도
    private int m_carCount = 0;//최대 차의 개수 난이도에 따라 증가
    private float m_carDistance = 9.0f;//차 사이의 거리

    public void GenerateCars()
    {
        m_difficulty = transform.parent.GetComponent<CarDrivingGame>().GetDifficulty();//진행중인 게임에서 난이도를 받아옴
        m_cars = new int[40];//차를 배치할 배열
        m_carCount = 10 + (m_difficulty * 3);//차의 개수 기본 10개에 난이도에 따라 증가
        m_carCount = m_carCount > m_cars.Length ? m_cars.Length : m_carCount;//차의 개수가 배열을 초과한 경우 최대로 고정

        for (int i = 0; i < m_cars.Length; i++)
        {
            m_cars[i] = 0;//CarPosition None 상태로 초기화
        }

        for (int i = 0; i < m_carCount; i++) //절반까지는 랜덤으로 채우고 나머지는 순서대로 채움
        {
            int index = Random.Range(0, m_cars.Length);

            if (m_cars[index] == 0)//None이 아닌 경우 건너뛰고 다른 위치에 배치
            {
                m_cars[index] = Random.Range(1, 3);//1이면 Left, 2이면 Right에 배치
            }
        }


        while (true)
        {
            int count = 0;

            //차의 개수를 셈
            for (int i = 0; i < m_cars.Length; i++)
            {
                count = m_cars[i] != 0 ? count + 1 : count;//차가 존재하는 경우 count++ 없으면 그대로
            }

            if (count < m_carCount)//존재해야 할 차의 개수가 적으면 배치
            {
                for (int i = 0; i < m_cars.Length; i++)
                {
                    if (m_cars[i] != 0) continue;//이미 자리가 있으면 통과

                    m_cars[i] = Random.Range(0, 3);//0이면 None, 1이면 Left, 2이면 Right에 배치
                    count = m_cars[i] != 0 ? count + 1 : count;

                    if (count >= m_carCount) break;//차의 개수가 충분하면 반복 탈출
                }
            }
            else
                break;//차의 개수가 충분하면 반복 탈출
        }

        //배열에 들어있는 차의 위치에 맞춰 생성
        for (int i = 0; i < m_cars.Length; i++)
        {

            if (m_cars[i] == (int)CarPosition.Left)
            {
                GameObject tmp = Instantiate(m_blueCar, transform);
                tmp.transform.position = new Vector3(1.5f, 0, 10.0f + (i * m_carDistance));
            }
            else if (m_cars[i] == (int)CarPosition.Right)
            {
                GameObject tmp = Instantiate(m_blueCar, transform);
                tmp.transform.position = new Vector3(-1.5f, 0, 10.0f + (i * m_carDistance));
            }
        }
       

    }

    enum CarPosition
    {
        None,//비어있는 칸
        Left,
        Right,
    }

}
