using UnityEngine;

class WallCollisions : MonoBehaviour
{
    [Header("Wall Collisions")]
    public GameObject m_left;
    public GameObject m_right;

    private float m_camSize = 10.0f;

    //테스트용
    private void Start()
    {
        SetWalls();
    }

    public void SetOrthographicSize(Camera camera)
    {
        m_camSize = camera.orthographicSize;
    }

    //사방면의 벽을 화면 크기에 맞춰서 옮기는 함수
    public void SetWalls()
    {
        float aspect = (float)Screen.width / Screen.height;
        float worldWidth = m_camSize * aspect;

        m_left.transform.position = new Vector3(-worldWidth, 0, 0);
        m_right.transform.position = new Vector3(worldWidth, 0, 0);
    }
}
