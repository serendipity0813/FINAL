using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUIController : MonoBehaviour
{
    public void OptionClick()
    {
        GameSceneManager.Instance.PopUpSelect(SCENES.OptionScene);
    }

}
