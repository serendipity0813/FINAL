using TMPro;
using UnityEngine;

public class TutorialUIController : MonoBehaviour
{
    public TMP_Text m_text;
    public int m_tutorialIndex;
    [SerializeField] private bool m_tutorialEnd;
    [SerializeField] private GameObject m_BG;
    [SerializeField] private GameObject m_SkipButton;
    [SerializeField] private GameObject m_DescriptionBG;
    [SerializeField] private GameObject m_progressBtn;
    [SerializeField] private GameObject m_arrow;
    [SerializeField] private GameObject m_touchProtection;
    [SerializeField] private RectTransform m_ArrowAnimation;
    private RectTransform m_rectTransformArrow;
    private RectTransform m_rectTransformDescriptionBG;
    private bool m_moveCheck = false;
    private bool eventCheck = false;
    public float m_tiem;

    private void Start()
    {
        m_tutorialEnd = PlayerDataManager.instance.m_playerData.tutorial;
        m_rectTransformArrow = m_arrow.GetComponent<RectTransform>();
        m_ArrowAnimation = m_ArrowAnimation.GetComponent<RectTransform>();
        m_rectTransformDescriptionBG = m_DescriptionBG.GetComponent<RectTransform>();
        m_tutorialIndex = 0;

        Invoke(nameof(StartTutorial), 0.5f);
    }
    private void StartTutorial()
    {
        if (!m_tutorialEnd)
        {
            TutorialTexts();
            SetAciveTrue();
            m_SkipButton.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        m_tiem += Time.deltaTime;

        ArrowMove();
        EventCheck();
    }
    private void ArrowMove()
    {
        if (m_moveCheck)
        {
            m_ArrowAnimation.anchoredPosition += new Vector2(0f, 0.2f);
            if (m_ArrowAnimation.anchoredPosition.y > 20)
                m_moveCheck = false;
        }
        else
        {
            m_ArrowAnimation.anchoredPosition += new Vector2(0f, -0.2f);
            if (m_ArrowAnimation.anchoredPosition.y < 0)
                m_moveCheck = true;
        }
    }
    private void EventCheck()
    {
        if (eventCheck)
        {
            if (m_tiem > 0.7f && m_tutorialIndex == 6)
            {
                SetAciveTrue();
                m_arrow.SetActive(true);
                Time.timeScale = 0f;
                eventCheck = false;
            }
            if (m_tiem > 1.7f && m_tutorialIndex == 7)
            {
                SetAciveTrue();
                m_arrow.SetActive(true);
                Time.timeScale = 0f;
                eventCheck = false;
            }
            if (m_tiem > 1.0f && m_tutorialIndex == 12)
            {
                SetAciveTrue();
                Time.timeScale = 0f;
                eventCheck = false;
            }
            if (m_tiem > 1.0f && m_tutorialIndex == 14)
            {
                SetAciveTrue();
                m_arrow.SetActive(true);
                Time.timeScale = 0f;
                eventCheck = false;
            }
            if (m_tiem > 0.2f && m_tutorialIndex == 17)
            {
                SetAciveTrue();
                Time.timeScale = 0f;
                eventCheck = false;
            }
            if (m_tiem > 1.0f && m_tutorialIndex == 19)
            {
                SetAciveTrue();
                m_arrow.SetActive(true);
                Time.timeScale = 0f;
                eventCheck = false;
            }
        }
    }
    // 텍스트를 불러올때
    public void TutorialTexts()
    {
        switch (m_tutorialIndex)
        {
            // 메인화면에서 부터 시작할 예정
            case 0:
                GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
                CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
                m_text.text = "이 게임이 처음이구나!\n튜토리얼에 온걸 환영해!";
                break;
            case 1:
                m_text.text = "지금은 게임의 메인 화면이야";
                break;
            case 2:
                m_text.text = "먼저 게임을 골라볼까?";
                m_arrow.SetActive(true);
                m_rectTransformArrow.anchoredPosition = new Vector2(-230f, -500f);
                m_BG.SetActive(false);
                break;
            // 셀렉트 게임
            case 3:
                GameSceneManager.Instance.PopupClear();
                GameSceneManager.Instance.SceneSelect(SCENES.SelectScene);
                CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
                m_BG.SetActive(true);
                m_arrow.SetActive(false);
                GameObject.Find("SelectScene(Clone)").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                m_text.text = "내가 가지고 있는 게임들이 표시될꺼야.";
                break;
            case 4:
                m_text.text = "아무거나 게임을 시작해볼까?";
                break;
            case 5:
                SetAciveFalse();
                MiniGameManager.Instance.GameReset();
                break;
            // 인게임
            case 6:
                m_tiem = 0;
                SetAciveFalse();
                eventCheck = true;
                m_rectTransformArrow.anchoredPosition = new Vector2(0f, 0f);
                m_rectTransformArrow.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                m_rectTransformDescriptionBG.anchoredPosition = new Vector2(0f, -500f);
                m_text.text = "게임이 시작되고\n몇 초 후\n간략한 설명이 적힌 팝업창이 나와";
                break;
            case 7:
                m_tiem = 0;
                SetAciveFalse();
                eventCheck = true;
                Time.timeScale = 1f;
                m_arrow.SetActive(false);
                m_rectTransformArrow.anchorMin = new Vector2(0f, 1f);
                m_rectTransformArrow.anchorMax = new Vector2(0f, 1f);
                m_rectTransformArrow.anchoredPosition = new Vector2(130f, -400f);
                m_rectTransformDescriptionBG.anchoredPosition = new Vector2(0f, 200f);
                m_text.text = "여기에는 게임의\n목표가 표시될꺼고";
                break;
            case 8:
                m_rectTransformArrow.anchorMin = new Vector2(0.5f, 0.5f);
                m_rectTransformArrow.anchorMax = new Vector2(0.5f, 0.5f);
                m_rectTransformArrow.anchoredPosition = new Vector2(0f, 600f);
                m_text.text = "여기에는 남은 시간이 표시되고 있어";
                break;
            case 9:
                m_text.text = "보통 게임들은\n시간이 0이 되면 패배할꺼야";
                break;
            case 10:
                m_text.text = "일단 게임을 진행해보자";
                m_arrow.SetActive(false);
                break;
            case 11:
                Time.timeScale = 1.0f;
                SetAciveFalse();
                eventCheck = true;
                break;
            //만약 게임에 지게된다면
            case 12:
                m_tiem = 0;
                eventCheck = true;
                SetAciveFalse();
                m_text.text = "원래라면 패배했지만 지금은 튜토리얼이니까";
                break;
            case 13:
                m_text.text = "승리로 바꿔버렸어";
                break;
            // 게임을 이기고 난 뒤
            case 14:
                Time.timeScale = 1.0f;
                if (m_text.text == "승리로 바꿔버렸어")
                {
                    SetAciveTrue();
                    Time.timeScale = 0f;
                    m_arrow.SetActive(true);
                    
                }
                else
                {
                    m_tiem = 0;
                    eventCheck = true;
                    SetAciveFalse();
                }
                
                m_rectTransformArrow.anchoredPosition = new Vector2(0f, -200f);
                m_rectTransformArrow.rotation = Quaternion.Euler(new Vector3(0f, 0f, -180f));
                m_text.text = "게임을 승리하면\n스테이지가 올라";
                break;
            case 15:
                m_text.text = "스테이지는\n점수를 뜻하기도 해";
                break;
            case 16:
                Time.timeScale = 1.0f;
                m_arrow.SetActive(false);
                SetAciveFalse();
                break;
            // 다음 게임에서 강제로 패배하게 만듦
            case 17:
                m_tiem = 0f;
                SetAciveFalse();
                eventCheck = true;
                m_text.text = "만약 게임에서\n진다면\n어떻게 되는지 알려줄께";
                break;
            case 18:
                Time.timeScale = 1.0f;
                SetAciveFalse();
                MiniGameManager.Instance.m_endCheck = true;
                MiniGameManager.Instance.GameFail();
                break;
            //패배
            case 19:
                m_tiem = 0f;
                eventCheck = true;
                SetAciveFalse();
                m_rectTransformArrow.anchoredPosition = new Vector2(0f, 0f);
                m_rectTransformArrow.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                m_rectTransformDescriptionBG.anchoredPosition = new Vector2(0f, -500f);
                m_text.text = "라이프가 하나씩 감소하게 되고\n0이 되면 게임이 끝나";
                break;
            // 메인화면으로 돌아옴
            case 20:
                m_arrow.SetActive(false);
                CameraManager.Instance.m_followEnabled = false;
                GameSceneManager.Instance.PopupClear();
                MiniGameManager.Instance.GameReset();
                Time.timeScale = 1.0f;
                GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
                CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
                m_rectTransformDescriptionBG.anchoredPosition = new Vector2(0f, 200f);
                m_text.text = "다시 메인화면으로 돌아왔어";
                break;
            case 21:
                m_text.text = "랜덤 게임에 대해 설명해줄께";
                m_arrow.SetActive(true);
                m_rectTransformArrow.rotation = Quaternion.Euler(new Vector3(0f, 0f, -180f));
                m_rectTransformArrow.anchoredPosition = new Vector2(0f, -500f);
                break;
            //랜덤게임 팝업창
            case 22:
                m_arrow.SetActive(false);
                MiniGameManager.Instance.GameNumber = -1;
                GameSceneManager.Instance.PopUpSelect(SCENES.GameChoiceScene);
                CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
                m_rectTransformDescriptionBG.anchoredPosition = new Vector2(0f, 0f);
                m_text.text = "랜덤 게임은 현재 내가 가지고 있는 게임들이";
                break;
            case 23:
                m_text.text = "모두 랜덤하게 나오게 되면서\n최대한 살아남는 게임이야";
                break;
            case 24:
                m_text.text = "가지고 있는 게임이 많아질수록\n더 재밌어질꺼야";
                break;
            case 25:
                m_text.text = "지금은 미니게임이 별로 없으니\n새로운 게임을 뽑으러 가보자";
                break;
            case 26:
                GameSceneManager.Instance.PopupClear();
                GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
                CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
                m_arrow.SetActive(true);
                m_rectTransformArrow.rotation = Quaternion.Euler(new Vector3(0f, 0f, -180f));
                m_rectTransformArrow.anchoredPosition = new Vector2(325f, 700f);
                m_rectTransformDescriptionBG.anchoredPosition = new Vector2(0f, 0f);
                m_text.text = "미니게임을 뽑기 할 수 있는 곳은 이곳이야";
                break;
            //랜덤게임 팝업창 닫기 이후 뽑기창으로
            case 27:
                m_arrow.SetActive(false);
                GameSceneManager.Instance.PopupClear();
                GameSceneManager.Instance.SceneSelect(SCENES.GatchaScene);
                CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
                m_rectTransformDescriptionBG.anchoredPosition = new Vector2(0f, 0f);
                GameObject.Find("GatchaScene(Clone)").gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                m_text.text = "여기는 미니게임을 뽑을 수 있는 곳이야";
                break;
            case 28:
                m_arrow.SetActive(true);
                m_BG.SetActive(false);
                m_rectTransformArrow.rotation = Quaternion.Euler(new Vector3(0f, 0f, -180f));
                m_rectTransformArrow.anchoredPosition = new Vector2(-255f, -500f);
                m_text.text = "뽑기를 한번 돌려보자";
                break;
            case 29:
                int haveGames = 0;
                m_arrow.SetActive(false);
                m_BG.SetActive(true);
                for(int i = 0; i < PlayerDataManager.instance.m_playerData.haveGames.Count; i++)
                {
                    if (PlayerDataManager.instance.m_playerData.haveGames[i] == true)
                    {
                        haveGames++;
                    }
                }
                if (haveGames >= 7)
                {
                    m_text.text = "이미 게임\n뽑았었구나";
                }
                else
                {
                    GatchaMiniGame gatchaMiniGame = gameObject.GetComponent<GatchaMiniGame>();
                    gatchaMiniGame.GatchaActiveBtn();
                    m_text.text = "새로운 미니게임을 뽑았어";
                }
                // 가차 구현 더해야함
                break;
            //뽑기창 닫고 메인화면으로
            case 30:
                GameSceneManager.Instance.PopupClear();
                GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
                CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
                m_arrow.SetActive(true);
                m_rectTransformArrow.anchoredPosition = new Vector2(-230f, -500f);
                m_BG.SetActive(false);
                m_text.text = "늘어난 게임을 확인하러 가볼까?";
                break;
            //선택 게임으로
            case 31:
                m_BG.SetActive(true);
                m_arrow.SetActive(false);
                GameSceneManager.Instance.PopupClear();
                GameSceneManager.Instance.SceneSelect(SCENES.SelectScene);
                CameraManager.Instance.ChangeCamera(CameraView.Angle90View);
                m_rectTransformDescriptionBG.anchoredPosition = new Vector2(0f, -500f);
                m_text.text = "5개였던 미니게임이 6개로 늘어났어";
                break;
            case 32:
                m_text.text = "이렇게 미니게임을 늘려가면서\n모든 미니게임을 모아보자";
                break;
            // 메인 화면으로
            case 33:
                GameSceneManager.Instance.PopupClear();
                GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
                CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
                m_text.text = "튜토리얼은 이렇게 끝났어\n끝까지 해줘서 고마워";
                break;
            case 34:
                m_text.text = "100코인을 추가로 더 넣어뒀어";
                if (!m_tutorialEnd)
                {
                    m_SkipButton.SetActive(false);
                    PlayerDataManager.instance.m_playerData.tutorial = true;
                    PlayerDataManager.instance.m_playerData.coin += 100;
                    GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
                    PlayerDataManager.instance.SaveJson();
                }
                break;
            case 35:
                m_text.text = "튜토리얼을 끝까지 해준 답례야";
                break;
            case 36:
                m_text.text = "그럼 안녕";
                break;
            case 37:
                Destroy(gameObject);
                break;
        }
    }

    public void SwitchButtonClick()
    {
        m_tutorialIndex++;
        TutorialTexts();
    }
    public void TutorialSkipClick()
    {
        transform.Find("Skip").gameObject.SetActive(true);
    }
    private void SetAciveFalse()
    {
        m_progressBtn.SetActive(false);
        m_BG.SetActive(false);
        m_DescriptionBG.SetActive(false);
    }
    private void SetAciveTrue()
    {
        m_progressBtn.SetActive(true);
        m_BG.SetActive(true);
        m_DescriptionBG.SetActive(true);
    }

    public void TutorialSkip()
    {
        //튜토리얼 보상
        if (!m_tutorialEnd)
        {
            PlayerDataManager.instance.m_playerData.tutorial = true;
            PlayerDataManager.instance.m_playerData.coin += 100;
            PlayerDataManager.instance.SaveJson();
        }

        CameraManager.Instance.m_followEnabled = false;
        GameSceneManager.Instance.PopupClear();
        MiniGameManager.Instance.GameReset();
        Time.timeScale = 1.0f;
        GameSceneManager.Instance.SceneSelect(SCENES.LobbyScene);
        CameraManager.Instance.ChangeCamera(CameraView.ZeroView);
        Destroy(gameObject);
    }
    public void TutorialNoSkip()
    {
        transform.Find("Skip").gameObject.SetActive(false);
    }
}
