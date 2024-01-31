using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIController : ButtonHandler
{
    [SerializeField] private GameObject m_pause;
    public GameObject Mission;

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
    }

}
