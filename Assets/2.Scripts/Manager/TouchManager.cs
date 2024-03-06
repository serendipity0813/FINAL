using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public static TouchManager instance;

    private bool m_dragDown;//아래로 드래그
    private bool m_dragUp;//위로 드래그
    private bool m_dragLeft;//왼쪽
    private bool m_dragRight;//오른쪽

    private bool m_isHolding;//손가락을 누르고 있는 상태
    private bool m_isBegan;//터치 시작했을 때

    private Vector2 m_position;//
    private Vector2 m_velocity;//드래그 속도
    private Touch m_touch;

    private float m_speed = 200.0f;//방향 결정 기준 속도


    private void Awake()
    {
        //터치 매니저 싱글톤화
        instance = this;
    }

    private void Update()
    {
        UpdateInput();
    }


    //Input에서 터치값을 받아와 이동 좌표와 방향 초기화하는 함수
    private void UpdateInput()
    {

        if (Input.touchCount > 0)
        {
            m_touch = Input.GetTouch(0);


            switch (m_touch.phase)
            {
                case TouchPhase.Began://터치 시작
                    m_isBegan = true;
                    m_isHolding = true;
                    m_position = m_touch.position;
                    m_velocity = Vector2.zero;
                    break;

                case TouchPhase.Ended://터치 끝
                    CheckDirection();
                    m_isHolding = false;
                    m_isBegan = false;
                    break;

                case TouchPhase.Moved://터치 드래그
                    m_position = m_touch.position;
                    m_velocity += m_touch.deltaPosition;
                    break;

            }
        }
    }

    //m_velocity 벡터의 방향에 따라 어느 방향으로 드래그하였는지 초기화하는 함수
    private void CheckDirection()
    {
        if (m_velocity.x < -m_speed)
        {
            m_dragLeft = true;
        }
        else
        {
            m_dragLeft = false;
        }

        if (m_velocity.x > m_speed)
        {
            m_dragRight = true;
        }
        else
        {
            m_dragRight = false;
        }

        if (m_velocity.y < -m_speed)
        {
            m_dragDown = true;
        }
        else
        {
            m_dragDown = false;
        }

        if (m_velocity.y > m_speed)
        {
            m_dragUp = true;
        }
        else
        {
            m_dragUp = false;
        }

    }

    public Vector2 GetPosition()
    {
        return m_position;
    }

    public bool IsHolding()
    {
        return m_isHolding;
    }

    public bool IsBegan()
    {
        if (m_isBegan)
        {
            m_isBegan = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDragLeft()
    {
        if (m_dragLeft)
        {
            m_dragLeft = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDragRight()
    {
        if (m_dragRight)
        {
            m_dragRight = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDragDown()
    {
        if (m_dragDown)
        {
            m_dragDown = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsDragUp()
    {
        if (m_dragUp)
        {
            m_dragUp = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
