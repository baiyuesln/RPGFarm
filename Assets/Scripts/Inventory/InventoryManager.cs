using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//物品仓库Manager
public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    private  Dictionary<int, ItemDetails> itemDetailsDicrionary;

    //列表数组
    public List<InventoryItem>[] inventoryLists;

    // 存储每个库存位置容量的数组
    [HideInInspector] public int[] inventoryListCapacityIntArray;

    [SerializeField] private SO_ItemList itemList = null;

    protected override void Awake()
    {
        base.Awake();
        //创建itemdetails 字典
        CreateItemDetailsDictionary();
        
        //创建库存列表
        CreateInventoryLists();
    }

    //创建不同位置的库存列表的方法
    private void CreateInventoryLists()
    {
        // 初始化不同库存位置的列表数组
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];

        // 初始化数组中的每个列表
        for (int i = 0;i<(int)InventoryLocation.count;i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }

        // 初始化每个库存位置的容量数组
        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];

        // 设置玩家库存位置的初始容量
        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    // 向库存中添加物品的方法，同时销毁传入的游戏对象
    public void AddItem(InventoryLocation inventoryLocation, Item item,GameObject gameObjectToDelete)
    {
        AddItem(inventoryLocation, item);
        Destroy(gameObjectToDelete);
    }

    // 向库存中添加物品的方法
    public void AddItem(InventoryLocation inventoryLocation,Item item)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        // 查找库存中是否已存在相同物品
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);
        if (itemPosition != -1)
        {
            // 如果存在，将物品添加到相同位置
            AddItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        else
        {
            // 如果不存在，将物品添加到新位置
            AddItemAtPosition(inventoryList, itemCode);
        }

        // 触发库存更新事件
        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    // 在指定位置添加物品的方法
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode)
    {
       InventoryItem inventoryItem = new InventoryItem();

        // 设置新物品的代码和数量
        inventoryItem.itemCode = itemCode;
        inventoryItem.itemQuantity = 1;
        inventoryList.Add(inventoryItem);

        DebugPrintInventoryList(inventoryList);
    }

    // 在指定位置添加物品的方法（用于已存在物品的情况）
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        // 获取已存在物品的数量，增加 1
        int quantity = inventoryList[position].itemQuantity + 1;
        inventoryItem.itemQuantity = quantity;
        inventoryItem.itemCode = itemCode;
        inventoryList[position] = inventoryItem;

        // 清空开发者控制台并输出库存列表
        Debug.ClearDeveloperConsole();
        DebugPrintInventoryList(inventoryList);
    }

    // 在库存中查找物品的方法
    private int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
        for(int i = 0;i<inventoryList.Count;i++)

            // 遍历库存列表，找到与物品代码匹配的物品位置
            if (inventoryList[i].itemCode == itemCode)
            {
                return i;
            }
    
        // 如果未找到，返回 -1
        return -1;
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

    public void RemoveItem(InventoryLocation inventoryLocation,int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
        int itemPosition = FindItemInInventory(inventoryLocation,itemCode);

        if(itemPosition != -1)
        {
            RemoveItemAtPosition(inventoryList, itemCode,itemPosition);
        }

        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    private void RemoveItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();
        int quantity = inventoryList[position].itemQuantity - 1;
        if(quantity > 0)
        {
            inventoryItem.itemQuantity = quantity;
            inventoryItem.itemCode = itemCode;
            inventoryList[position] = inventoryItem;
        }
        else
        {
            inventoryList.RemoveAt(position);
        }
    }

    // 在控制台中打印库存列表的调试方法
    private void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    {
        // 遍历库存列表中的每个库存物品
        foreach (InventoryItem inventoryItem in inventoryList)
        {
            // 获取物品详细信息并输出物品描述和数量到控制台
            Debug.Log("Item Description:" + 
                InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode).itemDescription 
                + "    Item Quantity: " + inventoryItem.itemQuantity);
        }
        Debug.Log("************************************************************");
    }
}
