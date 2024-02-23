using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            if (PlayerDataManager.instance.m_playerData.equipSkin[i] == true)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }

        //transform.GetChild(1).gameObject.SetActive(true);
    }

}
