
using UnityEngine;

public class JumpRopeGame : MiniGameSetting
{
    [SerializeField]
    private DragToMoveController m_dragToMoveController;

    private int m_count = 0;
    private int m_difficulty = 1;
    private bool m_collision = false;//플레이어가 밧줄과 닿았는지 체크하는 함수

    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_dragToMoveController.UpdateMoveWithJump();
    }

    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.Angle60View);//90도 각도로 내려다 보는 카메라로 변경
    }

    public void CheckWin()
    {
        bool result;

        switch (m_difficulty)
        {
            case 1:
                result = m_count > 5 ? true : false;
                break;
            case 2:
                result = m_count > 10 ? true : false;
                break;
            case 3:
                result = m_count > 15 ? true : false;
                break;
            default:
                //난이도가 필요없는 경우 무조건 지는 판정으로 
                result = false;
                break;
        }

        if (result)//개수 제한을 넘겼을 때 승리
        {
            GameClear();
        }
        else
        {
            if (m_collision)//개수 제한을 못넘기고 줄에 걸렸을 때 패배
            {
                GameFail();
            }
        }
    }

    public void SetCollision()
    {
        m_collision = true;
    }

    public void AddCount()
    {
        m_count++;
    }

    public void SetLevel(int difficulty)
    {
        m_difficulty = difficulty;
    }


}
