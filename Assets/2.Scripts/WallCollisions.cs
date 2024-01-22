using UnityEngine;

class WallCollisions : MonoBehaviour
{
    [Header("Wall Collisions")]
    public GameObject m_top;
    public GameObject m_bot;
    public GameObject m_left;
    public GameObject m_right;

    [SerializeField] private float m_topSpace;
    [SerializeField] private float m_botSpace;
    [SerializeField] private float m_leftSpace;
    [SerializeField] private float m_rightSpace;

    private Camera m_camera;
    private float m_camSize = 10.0f;

    private void Start()
    {
        m_camera = CameraManager.Instance.GetCamera();
        SetWalls();
    }

    public void SetOrthographicSize(Camera camera)
    {
        m_camSize = camera.orthographicSize;
    }

    //사방면의 벽을 화면 크기에 맞춰서 옮기는 함수
    public void SetWalls()
    {
        float cameraHeight = 2.0f * m_camSize; // 카메라 세로 크기
        float cameraWidth = cameraHeight * m_camera.aspect; // 카메라 가로 크기

        m_left.transform.position = new Vector3(-cameraWidth / 2.0f + m_leftSpace, 0.5f, 0.0f);
        m_right.transform.position = new Vector3(cameraWidth / 2.0f + m_rightSpace, 0.5f, 0.0f);

        m_top.transform.position = new Vector3(0.0f, 0.5f, cameraHeight / 2.0f + m_topSpace);
        m_bot.transform.position = new Vector3(0.0f, 0.5f, -cameraHeight / 2.0f + m_botSpace);
    }
}
