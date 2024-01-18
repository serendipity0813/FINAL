
using UnityEngine;

public class JumpRopeGame : MonoBehaviour
{
    [SerializeField]
    private DragToMoveController m_dragToMoveController;

    private int m_count;
    private int m_difficulty;

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_dragToMoveController.UpdateMoveWithJump();
    }

    private void CheckWin()
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


        if(result)
        {
            MiniGameManager.Instance.GameClear();
        }
        else
        {
            MiniGameManager.Instance.GameFail();
        }
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
