using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRopeGame : MonoBehaviour
{
    [SerializeField]
    private DragToMoveController m_dragToMoveController;

    // Update is called once per frame
    void Update()
    {
        m_dragToMoveController.UpdateMove();
    }

    public void GameOver()
    {

    }

    public void DestroyGame()
    {

    }
}
