using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "Item/ItemDataSO", order = 1)]
public class ItemDataSO : ScriptableObject
{
    [Serializable] 
    public struct Items
    {
        [Header("ItemData")]
        public string itemName; 
        public int itemIndex;
        public int itemPrice;
        public GameObject itemPrefab; 

    }
    public List<Items> items = new List<Items>();
}

