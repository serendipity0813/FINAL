using UnityEngine;

public enum CameraView
{
    ZeroView,
    Angle90View,
    Angle60View,
    Angle30View,
}

[DefaultExecutionOrder(-2)]

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField]
    private GameObject m_cameraBox;//모든 카메라가 담겨있는 오브젝트

    private Camera[] m_cameras;//m_cameraBox 안의 자식 카메라들을 순서대로 정리한 변수
    private CameraView m_cameraView;//카메라 순서를 enum으로 구별할 수 있게

    private GameObject m_targetObject;//따라가게 만들 오브젝트
    public bool m_followEnabled = false;//카메라를 따라가게 할지 확인하는 변수, false = 해제, true = 설정
    private float m_followSpeed = 1.0f;//카메라 이동 속도

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

        m_cameras = new Camera[m_cameraBox.transform.childCount];

        //카메라 박스 안의 자식개수만큼 반복
        for (int i = 0; i < m_cameraBox.transform.childCount; i++)
        {
            //카메라 담기
            m_cameras[i] = m_cameraBox.transform.GetChild(i).GetComponent<Camera>();
        }

        ChangeCamera(CameraView.ZeroView);//처음 시작화면의 카메라로 초기화
    }

    private void FixedUpdate()
    {
        CameraFollow();
    }

    //현재 활성화된 카메라를 리턴
    public Camera GetCamera()
    {
        int index = (int)m_cameraView;//현재 카메라를 번호로 변환

        if (m_cameras.Length - 1 < index)//카메라 개수를 초과한 경우 Zero뷰로 고정
        {
            index = 0;
        }

        return m_cameras[index];//현재 카메라 리턴
    }

    //카메라가 따라가는 속도 설정 함수
    public void SetFollowSpeed(float speed)
    {
        m_followSpeed = speed;
    }

    //카메라가 따라갈 타켓 지정 함수
    public void SetFollowTarget(GameObject target)
    {
        m_targetObject = target;
    }

    //카메라 따라가기 기능 토글 함수
    //카메라 따라가기 
    public void CameraFollow()
    {
        if (m_followEnabled)//따라가기 기능이 켜져있을 경우
        {
            if (m_targetObject != null) //타켓이 있을 때
                m_cameraBox.transform.position = Vector3.Slerp(m_cameraBox.transform.position, m_targetObject.transform.position, m_followSpeed * Time.deltaTime);
        }
    }

    //다른 카메라로 전환하는 함수
    public void ChangeCamera(CameraView view)
    {
        m_cameraView = view;
        int index = (int)m_cameraView;//현재 카메라를 번호로 변환

        //모든 카메라 끄기
        foreach (Camera cam in Camera.allCameras)
        {
            cam.enabled = false;
        }

        if (m_cameras.Length - 1 < index)//카메라 개수를 초과한 경우 Zero뷰로 고정
        {
            index = 0;
        }

        //해당 번호의 카메라 켜기
        m_cameras[index].enabled = true;

        m_followEnabled = false;
        m_cameraBox.transform.position = Vector3.zero;//원래 위치로 되돌리기
        m_targetObject = null;//타겟 초기화
    }
}
