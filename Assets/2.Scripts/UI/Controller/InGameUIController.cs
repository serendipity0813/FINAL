using UnityEngine;

public class InGameUIController : ButtonHandler
{
    [SerializeField] private GameObject m_pause;
    [SerializeField] private GameObject m_stop;

    public void PauseClick()
    {
        if (m_pause.activeSelf)
        {
            m_pause.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            m_pause.SetActive(true);
            Time.timeScale = 0.0f;
        }
        EffectSoundManager.Instance.PlayEffect(1);
    }
    public void StopClick()
    {
        if (m_stop.activeSelf)
        {
            m_stop.SetActive(false);
        }
        else
        {
            m_stop.SetActive(true);
        }
        EffectSoundManager.Instance.PlayEffect(1);
    }
    public void StopYesCilck()
    {
        Time.timeScale = 1.0f;
        MiniGameManager.Instance.GameSave();
        GameSceneManager.Instance.SceneSelect(SCENES.GameOverScene);
        Destroy(gameObject);
    }
}
