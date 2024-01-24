using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketGame : MiniGameSetting
{

    private int m_catchCounts = 0;//박스에 과일을 담은 개수
    private int m_difficulty = 1;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);//90도 각도로 내려다 보는 카메라로 변경
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //클리어 조건을 충족하였는지 체크하는 함수
    public bool CheckClear()
    {
        bool result = false;

        //난이도 만큼 잡아야 하는 과일 개수를 못채웠을 경우 false 리턴
        switch(m_difficulty)
        {
            case 1:
                result = m_catchCounts < 3 ? false : true;
                break;
            case 2:
                result = m_catchCounts < 5 ? false : true;
                break;
            case 3:
                result = m_catchCounts < 7 ? false : true;
                break;
        }

        return result;
    }

    public void SetLevel(int difficulty)
    {
        m_difficulty = difficulty;
    }
}
