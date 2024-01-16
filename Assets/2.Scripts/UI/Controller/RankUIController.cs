using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankUIController : MonoBehaviour
{
    public void RankClick()
    {
        UIManager.Instance.PopUpSelect(SCENES.RankScene);
    }
}
