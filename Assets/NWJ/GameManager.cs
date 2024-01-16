using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]//private 맴버변수도 inspecter창에서 수정 가능

    //private UserInterfaceManager m_uiManager;
    //private UserDataManager m_userDataManager;
    //private SelectModeManager m_selectGameManager;
    //private RandomModeManager m_randomModeManager;
    private CameraManager m_cameraManager;


    private void Awake()
    {
        instance = this;
    }


}
