using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIController : ButtonHandler
{
    [SerializeField] private GameObject m_buyCheck;
    [SerializeField] private GameObject m_fail;
    [SerializeField] private GameObject[] m_tabs;
    private string m_itemName = " ";
    private int m_itemCount = 0;
    private int m_itemPrice = 0;

    public void ItemClick(string name)
    {
        m_itemName = name;
        m_itemCount = 1;
        m_itemPrice = 100;
        m_buyCheck.SetActive(true);
    }

    public void BuyCheck()
    {
        m_buyCheck.SetActive(false);

        if(PlayerDataManager.instance.m_playerData.coin > m_itemPrice)
        {
            PlayerDataManager.instance.m_playerData.coin -= m_itemPrice;
            PlayerDataManager.instance.GetItem(m_itemName, m_itemCount);
        }
        else
            m_fail.SetActive(true);

    }

    public void ShopTaps(int tabNumber)
    {
        for(int i=0; i< m_tabs.Length; i++)
        {
            if(i == tabNumber)
                m_tabs[i].SetActive(true);
            else
                m_tabs[i].SetActive(false);
        }
    }

}
