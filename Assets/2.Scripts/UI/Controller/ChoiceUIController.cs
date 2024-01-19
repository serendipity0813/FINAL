using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceUIController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_gameNameText;
    //클릭한 게임에 대한 실시간 정보를 출력하도록 코드 필요
    private void Update()
    {
        m_gameNameText.text = MiniGameManager.Instance.GameName;
    }
}
