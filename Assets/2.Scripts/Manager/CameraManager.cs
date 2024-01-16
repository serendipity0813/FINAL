using UnityEngine;

public enum CameraView
{
    StartScene,
    MainScene,
    ShopScene,
    SelectMode,
    RandomMode,

    TopDownView,//위에서 아래로
    FrontView,//정면
    LeftView,//
    RightView,
}

public class CameraManager : MonoBehaviour
{

    [SerializeField]
    private Camera[] m_cameras;
    private CameraView m_cameraView;

    void Start()
    {
        foreach (Camera cam in m_cameras)//모든 카메라 비활성화
        {
            cam.enabled = false;
        }

        ChangeCamera(CameraView.StartScene);//처음 시작화면의 카메라로 초기화
    }

    //현재 활성화된 카메라를 리턴
    public Camera GetCamera()
    {
        return m_cameras[(int)m_cameraView];
    }

    //다른 카메라로 전환하는 함수
    public void ChangeCamera(CameraView view)
    {
        m_cameraView = view;

        foreach (Camera cam in m_cameras)
        {
            cam.enabled = false;
        }

        m_cameras[(int)view].enabled = true;
    }
}
