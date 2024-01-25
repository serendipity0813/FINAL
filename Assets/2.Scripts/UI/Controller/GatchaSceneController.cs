using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatchaSceneController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_playerTicketText;

    private void Start()
    {
        m_playerTicketText.text = "99";
    }

    public void AdClick()
    {
        Debug.Log("광고보기");
    }


}
