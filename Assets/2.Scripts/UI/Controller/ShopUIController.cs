using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIController : MonoBehaviour
{

    public void ShopClick()
    {
        UIManager.Instance.PopUpSelect(SCENES.ShopScene);
    }

}
