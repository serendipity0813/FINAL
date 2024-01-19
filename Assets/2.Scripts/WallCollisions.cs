using UnityEngine;

class WallCollisions : MonoBehaviour
{
    [Header("Wall Collisions")]
    public GameObject m_left;
    public GameObject m_right;
    public GameObject m_top;
    public GameObject m_bot;
    [SerializeField] private float m_leftSpace;
    [SerializeField] private float m_rightSpace;
    [SerializeField] private float m_topSpace;
    [SerializeField] private float m_botSpace;

    private Camera m_camera;

    private void Awake()
    {
        m_camera = Camera.main;
    }
    //테스트용
    private void Start()
    {
        SetWalls();
    }

    //사방면의 벽을 화면 크기에 맞춰서 옮기는 함수
    public void SetWalls()
    {
        float cameraHeight = 2f * m_camera.orthographicSize; // 카메라 세로 크기
        float cameraWidth = cameraHeight * m_camera.aspect; // 카메라 가로 크기

        m_left.transform.position = new Vector3(-cameraWidth / 2 + m_leftSpace, 0.5f, 0);
        m_right.transform.position = new Vector3(cameraWidth / 2 + m_rightSpace, 0.5f, 0);
        m_top.transform.position = new Vector3(0, 0.5f, cameraHeight / 2 + m_topSpace);
        m_bot.transform.position = new Vector3(0, 0.5f, -cameraHeight / 2 + m_botSpace);
    }
}
