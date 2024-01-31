using TMPro;
using UnityEngine;

public class MiniGameSetting : MonoBehaviour
{
    //ui오브젝트 필드
    protected GameObject m_missionUI;
    protected GameObject m_timeUI;
    protected GameObject m_countUI;
    protected GameObject m_clearUI;
    protected GameObject m_failUI;
    protected GameObject m_missionPrefab;
    protected GameObject m_timePrefab;
    protected GameObject m_countPrefab;
    protected GameObject m_clearPrefab;
    protected GameObject m_failPrefab;
    protected TextMeshProUGUI m_missionText;
    protected TextMeshProUGUI[] m_timeText;
    protected TextMeshProUGUI[] m_countText;

    protected virtual void Awake()
    {
        if (MiniGameManager.Instance != null)
        {

            //미니게임 매니저로부터 받아와 캐싱해두기
            m_missionUI = MiniGameManager.Instance.InGameUIs[0];
            m_timeUI = MiniGameManager.Instance.InGameUIs[1];
            m_countUI = MiniGameManager.Instance.InGameUIs[2];
            m_clearUI = MiniGameManager.Instance.InGameUIs[3];
            m_failUI = MiniGameManager.Instance.InGameUIs[4];

            //미니게임 오브젝트 하위에 생성되도록 하기
            m_missionPrefab = Instantiate(m_missionUI, transform.position, Quaternion.identity, transform);
            m_timePrefab = Instantiate(m_timeUI, transform.position, Quaternion.identity, transform);
            m_countPrefab = Instantiate(m_countUI, transform.position, Quaternion.identity, transform);
            m_clearPrefab = Instantiate(m_clearUI, transform.position, Quaternion.identity, transform);
            m_failPrefab = Instantiate(m_failUI, transform.position, Quaternion.identity, transform);

            //UI오브젝트 하위의 Text오브젝트의 컴포넌트 캐싱해두기, time과 count는 2개 받아와서 0번은 설명, 1번은 숫자값
            m_missionText = m_missionPrefab.GetComponentInChildren<TextMeshProUGUI>();
            m_timeText = m_timePrefab.GetComponentsInChildren<TextMeshProUGUI>();
            m_countText = m_countPrefab.GetComponentsInChildren<TextMeshProUGUI>();

            //초기에는 false로 설정되도록 
            m_missionPrefab.SetActive(false);
            m_timePrefab.SetActive(false);
            m_countPrefab.SetActive(false);
            m_clearPrefab.SetActive(false);
            m_failPrefab.SetActive(false);
        }
    }

    //게임 클리어, 실패 함수
    protected void GameClear()
    {
        MiniGameManager.Instance.GameClear();
    }

    protected void GameFail()
    {
        MiniGameManager.Instance.GameFail();
    }


}
