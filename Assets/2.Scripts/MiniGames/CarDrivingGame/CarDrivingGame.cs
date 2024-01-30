using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrivingGame : MiniGameSetting
{
    [SerializeField] private GameObject m_player;

    private CharacterController m_playerController;
    private CarGenerator m_generator;
    private int m_difficulty = 1;

    private float m_runningSpeed = 0.4f;
    private float m_sideSpeed = 0.2f;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        CameraManager.Instance.SetFollowSpeed(10.0f);
        CameraManager.Instance.SetFollowTarget(m_player);
        CameraManager.Instance.ToggleCameraFollow();

        m_playerController = m_player.GetComponent<CharacterController>();
        m_generator = transform.GetChild(2).GetComponent<CarGenerator>();
        m_generator.GenerateCars();//난이도에 맞춰서 차 생성

    }

    private void FixedUpdate()
    {

        try
        {
            m_playerController.Move(new Vector3(0, 0, m_runningSpeed));

            if (TouchManager.instance.IsHolding())
            {
                float direction = Input.mousePosition.x - ((float)Screen.width / 2);//화면을 절반으로 나누어 왼쪽, 오른쪽 부분 어디를 선택하였는지 체크

                if (direction > 0)
                {
                    m_playerController.Move(new Vector3(m_sideSpeed, 0, 0));
                }
                else
                {
                    m_playerController.Move(new Vector3(-m_sideSpeed, 0, 0));
                }
            }
        }
        catch
        {
            Debug.Log("Player's Rigidbody is Null");
        }

    }

    public int GetDifficulty()
    {
        return m_difficulty;
    }

    public void Win()
    {
        CameraManager.Instance.ToggleCameraFollow();
        GameClear();
    }

    public void Lose()
    {
        CameraManager.Instance.ToggleCameraFollow();
        GameFail();
    }


}
