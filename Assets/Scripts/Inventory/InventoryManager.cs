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
        //创建itemdetails 字典
        CreateItemDetailsDictionary();

    }

    /// <summary>
    /// 从scriptable object列表中填充itemDetailsDictionary
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
    /// 根据itemCode返回itemDetails(来自SO_ItemList)，返回null表示item代码不存在
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
