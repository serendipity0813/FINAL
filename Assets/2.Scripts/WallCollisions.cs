using UnityEngine;

class WallCollisions : MonoBehaviour
{
    [Header("Wall Collisions")]
    public GameObject m_top;
    public GameObject m_bot;
    public GameObject m_left;
    public GameObject m_right;

    private float m_topSpace;//화면 위쪽 남는 공간 크기

    private Camera m_camera;
    private float m_camSize = 10.0f;//현재 카메라의 사이즈

    private void Start()
    {
        m_camera = CameraManager.Instance.GetCamera();
        m_camSize = m_camera.orthographicSize;
        m_topSpace = Screen.safeArea.height / Screen.height;//기본 스크린 높이는 int형 주의

        SetWalls();
    }

    public void SetOrthographicSize(Camera camera)
    {
        m_camSize = camera.orthographicSize;
    }

    //사방면의 벽을 화면 크기에 맞춰서 옮기는 함수
    public void SetWalls()
    {
        float cameraWidth = m_camSize * m_camera.aspect; // 카메라 가로 크기

        m_left.transform.position = new Vector3(-cameraWidth , 0.5f, 0.0f);
        m_right.transform.position = new Vector3(cameraWidth , 0.5f, 0.0f);

        m_top.transform.position = new Vector3(0.0f, 0.5f, m_camSize * m_topSpace);
        m_bot.transform.position = new Vector3(0.0f, 0.5f, -m_camSize);
    }
}
