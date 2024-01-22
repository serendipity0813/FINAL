using UnityEngine;

public enum CameraView
{
    ZeroView,
    Angle90View,
    Angle60View,
    Angle30View,
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField]
    private GameObject m_cameraBox;//모든 카메라가 담겨있는 오브젝트

    private Camera[] m_cameras;//m_cameraBox 안의 자식 카메라들을 순서대로 정리한 변수
    private CameraView m_cameraView;//카메라 순서를 enum으로 구별할 수 있게

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    void Start()
    {
        m_cameras = new Camera[m_cameraBox.transform.childCount];

        //카메라 박스 안의 자식개수만큼 반복
        for (int i = 0; i < m_cameraBox.transform.childCount; i++)
        {
            //카메라 담기
            m_cameras[i] = m_cameraBox.transform.GetChild(i).GetComponent<Camera>();
        }


        ChangeCamera(CameraView.ZeroView);//처음 시작화면의 카메라로 초기화
    }

    //현재 활성화된 카메라를 리턴
    public Camera GetCamera()
    {
        int index = (int)m_cameraView;//현재 카메라를 번호로 변환
        return m_cameras[index];//현재 카메라 리턴
    }

    //다른 카메라로 전환하는 함수
    public void ChangeCamera(CameraView view)
    {
        m_cameraView = view;

        //모든 카메라 끄기
        foreach (Camera cam in m_cameras)
        {
            cam.enabled = false;
        }

        //해당 번호의 카메라 켜기
        m_cameras[(int)view].enabled = true;
    }
}
