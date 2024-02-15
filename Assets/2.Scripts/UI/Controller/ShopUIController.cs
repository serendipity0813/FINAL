using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUIController : ButtonHandler
{
    [SerializeField] private TextMeshProUGUI m_itemNameText;
    [SerializeField] private TextMeshProUGUI m_itemPriceText;
    [SerializeField] private GameObject m_buyCheck;
    [SerializeField] private GameObject m_equipCheck;
    [SerializeField] private GameObject m_success;
    [SerializeField] private GameObject m_fail;
    [SerializeField] private GameObject[] m_tabs;
    [SerializeField] private GameObject[] m_haveMarks;
    [SerializeField] private GameObject[] m_equipMarks;
    private int m_itemCode = 0;
    private int m_itemPrice = 0;


    private void Start()
    {
        PlayerAvatarCheck();
    }

    public void ShopTaps(int num)
    {
        for (int i = 0; i < m_tabs.Length; i++)
        {
            if (i == num)
                m_tabs[i].SetActive(true);
            else
                m_tabs[i].SetActive(false);
        }
    }


    public void ItemClick(int itemCode)
    {
        m_itemCode = itemCode;
        if (itemCode >= 1000)
        {
            BuyCoin();
        }
        else if(itemCode >= 100)
        {
            BuyBuff();
        }
        else
        {
            if(PlayerDataManager.instance.m_playerData.haveSkin[m_itemCode] && !PlayerDataManager.instance.m_playerData.equipSkin[m_itemCode])
            {
                m_equipCheck.SetActive(true);
            }
            else if(!PlayerDataManager.instance.m_playerData.haveSkin[m_itemCode])
            {
                m_itemNameText.text = PlayerDataManager.instance.ItemData.items[itemCode].itemName + "을";
                m_itemPrice = PlayerDataManager.instance.ItemData.items[itemCode].itemPrice;
                m_itemPriceText.text = m_itemPrice.ToString() + "원에";
                m_buyCheck.SetActive(true);
            }
   
        }
     
    }

    public void BuyCheck()
    {
        m_buyCheck.SetActive(false);

        if(PlayerDataManager.instance.m_playerData.coin >= m_itemPrice)
        {
            PlayerDataManager.instance.m_playerData.coin -= m_itemPrice;
            PlayerDataManager.instance.GetItem(m_itemCode);
            PlayerAvatarCheck();
        }
        else
            m_fail.SetActive(true);

    }

    public void ChangeAvatar()
    {

        PlayerDataManager.instance.EquipItem(m_itemCode);
        PlayerAvatarCheck();
    }

    public void BuyCoin()
    {
        //PlayerDataManager.instance.GetCoin(m_itemCode);
        m_success.SetActive(true);
    }

    public void BuyBuff()
    {
        m_success.SetActive(true);
    }

    private void PlayerAvatarCheck()
    {
        for (int i = 0; i < m_haveMarks.Length; i++)
        {
            if (PlayerDataManager.instance.m_playerData.haveSkin[i])
                m_haveMarks[i].SetActive(true);
            else
                m_haveMarks[i].SetActive(false);
        }

        for (int i=0; i< m_equipMarks.Length; i++)
        {
            if (PlayerDataManager.instance.m_playerData.equipSkin[i])
            {
                m_haveMarks[i].SetActive(false);
                m_equipMarks[i].SetActive(true);
            }

            else
                m_equipMarks[i].SetActive(false);
        }


    }

}
