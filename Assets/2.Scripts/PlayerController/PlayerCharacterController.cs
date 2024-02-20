using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController Instance;    //싱글톤
    private GameObject m_positionObject;
    private int m_avatarNumber;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangePlayerAvatar()
    {
        for (int i = 0; i < 10; i++)
        {
            if (PlayerDataManager.instance.m_playerData.equipSkin[i] == true)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                Debug.Log(transform.GetChild(i).gameObject.name);
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ChangePlayerPosition()
    {
        m_positionObject = GameObject.FindWithTag("PlayerPosition");
        if(m_positionObject != null )
        this.transform.position = m_positionObject.transform.position;
    }
}
