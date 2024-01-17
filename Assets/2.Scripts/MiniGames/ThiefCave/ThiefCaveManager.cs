using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ThiefCaveManager : MonoBehaviour
{
    public static ThiefCaveManager Instance;
    private int stage = 3;
    private float m_timer;
    public Vector3[] m_hidePosition { get; private set; }   //동굴 위치를 나타내는 백터 배열
    public bool IsGaiming { get; private set; }     //게임 진행중임을 체크
    public bool IsChanging { get; private set; }    //도둑들이 다음 경로를 찾아야하는지 여부 체크용
    public bool GameOver { get; private set; }
    private bool m_clear;
    public GameObject Target;
    public GameObject Thief;
    public GameObject Cave;
    public GameObject Mission;
    public GameObject Clear;
    public GameObject Fail;

    private void Awake()
    {
        //싱글톤화
        if(Instance == null)
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
        m_clear = false;
        IsGaiming = false;
        IsChanging = true;
        m_hidePosition = new Vector3[stage * 2 + 1];

        //랜덤한 위치에 도둑이 숨을 공간 생성
        for (int i = 0; i <= stage * 2 ; i++)
        {
            m_hidePosition[i] = new Vector3((int)Random.Range(-2, 3) * 2, 0, (int)Random.Range(-2, 5) * 2);
            if(i == 0)
            {
                //첫 번째 생성할 때 타겟도 생성
                Target.transform.position = m_hidePosition[i];
                Instantiate(Target);
            }
            Cave.transform.position = m_hidePosition[i];
            Instantiate(Cave);

            //i가 홀수일 때 마다 타겟이 아닌 오브젝트 생성
            if(i%2 == 1)
            {
                Thief.transform.position = m_hidePosition[i];
                Instantiate(Thief);
            }
        }

    }

    private void Update()
    {
        //시간 흐름에 따라 게임 진행 
        m_timer += Time.deltaTime;
        if (m_timer > 0.5 && Mission.activeSelf == false)
        {
            Mission.SetActive(true);
        }
        if (m_timer > 1.5 && Mission.activeSelf == true)
        {
            Mission.SetActive(false);
        }
        if(m_timer > 2)
        {
            //게임 진행이 시작되며 도둑들이 움직임
            IsGaiming = true;
        }
        if (m_timer > 7)
        {
            //도둑은 움직이지만 다음 경로를 찾는 행위는 중지됨
            IsChanging = false;
        }
        if (m_timer > 9)
        {   
            //도둑들이 멈추고 플레이어가 타겟을 찾기 시작
            IsGaiming = false;

            if(Input.GetMouseButtonDown(0))
            {
                // 마우스 클릭시 RAY를 활용하여 타겟 찾기
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Target")
                    {
                        Clear.SetActive(true);
                        Invoke("GameClear", 1);
                    }
                    else
                    {
                        Fail.SetActive(true); 
                        Invoke("GameFail", 1);
                    }
                }
            }

        }

        if (m_timer > 15 && m_clear == false)
        {
            Fail.SetActive(true);
            Invoke("GameFail", 1);
        }

    }

    private void GameClear()
    {
        Clear.SetActive(false);
        MiniGameManager.Instance.GameClear();
    }

    public void GameFail()
    {
        Fail.SetActive(false);
        MiniGameManager.Instance.GameFail();
    }

}
