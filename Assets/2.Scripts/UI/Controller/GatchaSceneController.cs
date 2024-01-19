using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatchaSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerCoinText;

    private void Start()
    {
        m_playerCoinText.text = "9999";
    }

    public void AdClick()
    {
        Debug.Log("광고보기");
    }


}
