using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUIController : ButtonHandler
{
    [SerializeField] private GameObject m_buyCheck;
    [SerializeField] private TextMeshProUGUI m_itemNameText;
    [SerializeField] private TextMeshProUGUI m_itemPriceText;
    [SerializeField] private GameObject m_fail;
    [SerializeField] private GameObject[] m_tabs;
    private int m_itemCode = 0;
    private int m_itemPrice = 0;


    public void ItemClick(int itemCode)
    {
        m_itemCode = itemCode;
        m_itemNameText.text = PlayerDataManager.instance.ItemData.items[itemCode].itemName;
        m_itemPrice = PlayerDataManager.instance.ItemData.items[itemCode].itemPrice;
        m_itemPriceText.text = m_itemPrice.ToString();
        m_buyCheck.SetActive(true);
    }

    public void BuyCheck()
    {
        m_buyCheck.SetActive(false);

        if(PlayerDataManager.instance.m_playerData.coin > m_itemPrice)
        {
            PlayerDataManager.instance.m_playerData.coin -= m_itemPrice;
            PlayerDataManager.instance.GetItem(m_itemCode);
        }
        else
            m_fail.SetActive(true);

    }

    public void ShopTaps(int num)
    {
        for(int i=0; i< m_tabs.Length; i++)
        {
            if(i == num)
                m_tabs[i].SetActive(true);
            else
                m_tabs[i].SetActive(false);
        }
    }

}
