using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChangeSceneController : MonoBehaviour
{
    public void OptionClick()
    {
        UIManager.Instance.PopUpSelect(SCENES.OptionScene);
    }
}
