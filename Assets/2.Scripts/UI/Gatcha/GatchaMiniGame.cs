using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GatchaMiniGame : MonoBehaviour
{
    [SerializeField] private Button m_gatchaBtn;
    [SerializeField] private GameObject m_warning;
    [SerializeField] private TextMeshProUGUI m_warningText;
    [SerializeField] private GameObject m_reward;
    [SerializeField] private TextMeshProUGUI m_rewardText;
    [SerializeField] private GameObject m_SpawnPosition;
    [SerializeField] private GameObject[] m_miniGameIcons;
    private GameObject m_gameIconPrefab;

    private int m_price = 1000;

    private void Awake()
    {
        m_gatchaBtn = GetComponent<Button>();
        if (m_gatchaBtn != null)
        m_gatchaBtn.onClick.AddListener(GatchaActiveBtn);
    }

    public void GatchaActiveBtn()
    {
        int gameCount = 0;
        bool haveAllGame = true;
        // 현재 플레이어 데이터에서 게임을 모두 가지고 있는지 체크
        foreach (bool havegame in PlayerDataManager.instance.m_playerData.haveGames)
        {
            if (havegame)
            {
                gameCount++;
            }
            else
            {
                // 하나라도 가지고 있지 않다면 false 이후 break
                haveAllGame = false;
                break;
            }
        }
        // 게임을 모두 가지고 있다면 메소드 종료
        if (haveAllGame)
        {
            m_warningText.text = "게임을 모두 가지고 있습니다.";
            m_warning.SetActive(true);
            //Debug.Log("게임을 모두 가지고 있습니다.");
            return;
        }

        // 코인이 있을 시 가차,없을 시 작동X
        if (PlayerDataManager.instance.m_playerData.coin >= m_price)
        {
            PlayerDataManager.instance.m_playerData.coin -= m_price;
            GatchaLogic();
        }
        else
        {
            m_warningText.text = "코인이 부족합니다.";
            m_warning.SetActive(true);
            //Debug.Log("코인이 부족합니다.");
        }
    }

    void GatchaLogic()
    {
        while (true)
        {
            int rnd = Random.Range(0, PlayerDataManager.instance.m_playerData.haveGames.Count);

            if (PlayerDataManager.instance.m_playerData.haveGames[rnd] == false)
            {
                PlayerDataManager.instance.m_playerData.haveGames[rnd] = true;
                m_gameIconPrefab = Instantiate(m_miniGameIcons[rnd], m_SpawnPosition.transform.position, Quaternion.identity, transform);
                PlayerDataManager.instance.SaveJson();
                m_rewardText.text = MiniGameManager.Instance.MiniGames.games[rnd].gameName + "게임을 얻었습니다.";
                m_reward.SetActive(true);
                //Debug.Log(MiniGameManager.Instance.MiniGames.games[rnd].gameName + "게임을 얻었습니다.");
                break;
            }
        }
    }

    public void OkBtn()
    {
        m_reward.SetActive(false);
        Destroy(m_gameIconPrefab);

    }
}
