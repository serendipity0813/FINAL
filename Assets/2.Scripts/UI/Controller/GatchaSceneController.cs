using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatchaSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerMoneyText;

    private void Start()
    {
        m_playerMoneyText.text = PlayerDataManager.instance.m_playerData.coin.ToString();
    }

    public void AdClick()
    {
        Debug.Log("광고보기");
    }


}
