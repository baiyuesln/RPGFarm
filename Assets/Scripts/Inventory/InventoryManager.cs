using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    private  Dictionary<int, ItemDetails> itemDetailsDicrionary;

    [SerializeField] private SO_ItemList itemList = null;

    private void Start()
    {
        //����itemdetails �ֵ�
        CreateItemDetailsDictionary();

    }

    /// <summary>
    /// ��scriptable object�б������itemDetailsDictionary
    /// </summary>
    public void CreateItemDetailsDictionary()
    {
        itemDetailsDicrionary = new Dictionary<int, ItemDetails>();

        foreach(ItemDetails itemDetails in itemList.itemDetails)
        {
            itemDetailsDicrionary.Add(itemDetails.itemCode,itemDetails);
        }
    }

    /// <summary>
    /// ����itemCode����itemDetails(����SO_ItemList)������null��ʾitem���벻����
    /// </summary>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemDetails;
        
        if(itemDetailsDicrionary.TryGetValue(itemCode, out itemDetails))
        {
            return itemDetails;
        }
        else
        {
            return null;
        }
    }
}
