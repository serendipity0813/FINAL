using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSceneAnimation : MonoBehaviour
{
    public void ShopReadySoundEffect()
    {
        EffectSoundManager.Instance.PlayEffect(19);

    }

    public void ShopOpenSoundEffect()
    {
        EffectSoundManager.Instance.PlayEffect(37);
    }
}
