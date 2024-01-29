using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SCENES
{
    //  0~5 : 게임 씬들, 6~9 : PopUp UI들
    StartScene = 0,
    LobbyScene,
    SelectScene,
    GatchaScene,
    GameChangeScene,
    InGameScene,
    GameOverScene,
    GameChoiceScene,
    OptionScene,
    ShopScene,
}

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;    //싱글톤
    public GameObject[] Scenes;     //씬 ui들 배열
    private GameObject m_scenePrefab;       //화면전환시 프리펩 생성 및 삭제를 위한 캐싱
    private GameObject m_PopupUI;
    private GameObject m_currentScene;      //현재 씬과 클릭으로 고른 씬 분리를 위함
    private GameObject m_choiceScene;

    private int m_popupNumber;      //팝업 ui와 씬 ui를 숫자로 분류하여 관리하기 위함
    private int m_sceneNumber;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {   
        //현재 선택된 화면이 없는 경우 첫 화면을 생성 및 현재 화면으로 저장
        if (m_currentScene == null)
        {
            m_currentScene = Scenes[0];
            m_scenePrefab = Instantiate(m_currentScene);
        }

    }

    public void SceneSelect(SCENES scene)
    {
        /*
        1. 매개변수로 전달받은 열겨형 변수를 int 숫자형으로 변환
        2. 매개변수를 통해 플레이어가 현재 선택한 화면을 판단 후 씬 배열에서 찾아 선택씬 변수에 캐싱
        3. 이전 씬 프리펩 삭제
        4. 선택한 씬을 프리펩 변수에 저장하면서 생성
        5. 현재 씬 변수를 선택씬 변수로 변경함
        6. 선택한 씬 변수에 따라서 씬에 필요한 오브젝트를 관리하는 함수 호출
         */
        m_sceneNumber = (int)scene;
        m_choiceScene = Scenes[m_sceneNumber];
        Destroy(m_scenePrefab);
        m_scenePrefab = Instantiate(m_choiceScene);
        m_currentScene = m_choiceScene;
    }


    public void PopUpSelect(SCENES scene)
    {
        //전달받은 매개변수에 따라 팝업 ui를 생성하거나, 지우거나, 지우고 새로운 팝업창을 띄우는 매커니즘
        //번호가 0인지, 같은지, 다른지에 따라 구분

        if (m_popupNumber == 0)
        {
            m_popupNumber = (int)scene;
            m_PopupUI = Instantiate(Scenes[m_popupNumber]);
        }
        else if(m_popupNumber == (int)scene)
        {
            Destroy(m_PopupUI);
            m_popupNumber = 0;
        }
        else
        {
            Destroy(m_PopupUI);
            m_popupNumber = (int)scene;
            m_PopupUI = Instantiate(Scenes[m_popupNumber]);
        }

    }

    public void PopupClear()
    {
        Destroy(m_PopupUI);
        m_popupNumber = 0;
    }



}
