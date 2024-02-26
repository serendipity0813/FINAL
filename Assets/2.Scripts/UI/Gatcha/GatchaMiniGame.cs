using System.Collections;
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
    [SerializeField] private Button m_OkButton;
    //[SerializeField] private GameObject[] m_miniGameIcons;
    private GameObject m_gameIconPrefab;
    private Animator gachaAnimation;

    private int m_price = 1000;

    private void Awake()
    {
        m_gatchaBtn = GetComponent<Button>();
        if (m_gatchaBtn != null)
            m_gatchaBtn.onClick.AddListener(GatchaActiveBtn);
    }
    private void FixedUpdate()
    {
        if (m_gameIconPrefab != null)
        {
            m_gameIconPrefab.transform.GetChild(0).Rotate(Vector3.up, 100f * Time.deltaTime);
            m_gameIconPrefab.transform.GetChild(0).Rotate(Vector3.right, 50f * Time.deltaTime);
            if (m_gameIconPrefab.transform.position.y <= 2.5f)
            {
                m_gameIconPrefab.transform.Translate(Vector3.up * 3f * Time.deltaTime);
            }
            if (m_gameIconPrefab.transform.position.z >= -8f)
            {
                m_gameIconPrefab.transform.Translate(Vector3.back * 3f * Time.deltaTime);
            }
        }
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
            EffectSoundManager.Instance.PlayEffect(2);
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
            EffectSoundManager.Instance.PlayEffect(2);
            //Debug.Log("코인이 부족합니다.");
        }
    }
    void GatchaLogic()
    {
        gachaAnimation = GameObject.Find("Chest_Animated").GetComponent<Animator>();
        gachaAnimation.SetTrigger("GachaPlay");
        while (true)
        {
            int rnd = Random.Range(0, PlayerDataManager.instance.m_playerData.haveGames.Count);

            if (PlayerDataManager.instance.m_playerData.haveGames[rnd] == false)
            {
                PlayerDataManager.instance.m_playerData.haveGames[rnd] = true;
                //m_gameIconPrefab = Instantiate(m_miniGameIcons[rnd], m_SpawnPosition.transform.position, Quaternion.identity, m_SpawnPosition.transform);
                if (m_SpawnPosition != null)
                    m_gameIconPrefab = Instantiate(MiniGameManager.Instance.MiniGames.games[rnd].gameIcon, m_SpawnPosition.transform.position, Quaternion.identity, m_SpawnPosition.transform);
                else
                    m_gameIconPrefab = Instantiate(MiniGameManager.Instance.MiniGames.games[rnd].gameIcon, GameObject.Find("SpawnPosition").transform.position, Quaternion.identity, GameObject.Find("SpawnPosition").transform);
                PlayerDataManager.instance.SaveJson();
                m_gameIconPrefab.GetComponent<SphereCollider>().enabled = false;
                m_gameIconPrefab.GetComponent<ConstantForce>().enabled = false;
                m_gameIconPrefab.GetComponent<Rigidbody>().useGravity = false;
                m_rewardText.text = MiniGameManager.Instance.MiniGames.games[rnd].gameName + "게임을 얻었습니다.";
                EffectSoundManager.Instance.PlayEffect(36);
                StartCoroutine(OkBtnCoroutine()); // 코루틴 실행
                //Debug.Log(MiniGameManager.Instance.MiniGames.games[rnd].gameName + "게임을 얻었습니다.");
                break;
            }
        }
    }

    public void OkBtn()
    {
        m_OkButton.gameObject.SetActive(false);
        m_reward.SetActive(false);
        Destroy(m_gameIconPrefab);
    }
    IEnumerator OkBtnCoroutine()
    {
        m_reward.SetActive(true);
        yield return new WaitForSeconds(3f);
        m_OkButton.gameObject.SetActive(true);
        m_OkButton.onClick.AddListener(OkBtn);
        yield break;
    }
}
