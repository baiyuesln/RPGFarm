using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ʒ�ֿ�Manager
public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    private  Dictionary<int, ItemDetails> itemDetailsDicrionary;

    //�б�����
    public List<InventoryItem>[] inventoryLists;

    // �洢ÿ�����λ������������
    [HideInInspector] public int[] inventoryListCapacityIntArray;

    [SerializeField] private SO_ItemList itemList = null;

    protected override void Awake()
    {
        base.Awake();
        //����itemdetails �ֵ�
        CreateItemDetailsDictionary();
        
        //��������б�
        CreateInventoryLists();
    }

    //������ͬλ�õĿ���б�ķ���
    private void CreateInventoryLists()
    {
        // ��ʼ����ͬ���λ�õ��б�����
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];

        // ��ʼ�������е�ÿ���б�
        for (int i = 0;i<(int)InventoryLocation.count;i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }

        // ��ʼ��ÿ�����λ�õ���������
        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];

        // ������ҿ��λ�õĳ�ʼ����
        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    // �����������Ʒ�ķ�����ͬʱ���ٴ������Ϸ����
    public void AddItem(InventoryLocation inventoryLocation, Item item,GameObject gameObjectToDelete)
    {
        AddItem(inventoryLocation, item);
        Destroy(gameObjectToDelete);
    }

    // �����������Ʒ�ķ���
    public void AddItem(InventoryLocation inventoryLocation,Item item)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        // ���ҿ�����Ƿ��Ѵ�����ͬ��Ʒ
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);
        if (itemPosition != -1)
        {
            // ������ڣ�����Ʒ��ӵ���ͬλ��
            AddItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        else
        {
            // ��������ڣ�����Ʒ��ӵ���λ��
            AddItemAtPosition(inventoryList, itemCode);
        }

        // �����������¼�
        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    // ��ָ��λ�������Ʒ�ķ���
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode)
    {
       InventoryItem inventoryItem = new InventoryItem();

        // ��������Ʒ�Ĵ��������
        inventoryItem.itemCode = itemCode;
        inventoryItem.itemQuantity = 1;
        inventoryList.Add(inventoryItem);

        DebugPrintInventoryList(inventoryList);
    }

    // ��ָ��λ�������Ʒ�ķ����������Ѵ�����Ʒ�������
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        // ��ȡ�Ѵ�����Ʒ������������ 1
        int quantity = inventoryList[position].itemQuantity + 1;
        inventoryItem.itemQuantity = quantity;
        inventoryItem.itemCode = itemCode;
        inventoryList[position] = inventoryItem;

        // ��տ����߿���̨���������б�
        Debug.ClearDeveloperConsole();
        DebugPrintInventoryList(inventoryList);
    }

    // �ڿ���в�����Ʒ�ķ���
    private int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
        for(int i = 0;i<inventoryList.Count;i++)

            // ��������б��ҵ�����Ʒ����ƥ�����Ʒλ��
            if (inventoryList[i].itemCode == itemCode)
            {
                return i;
            }
    
        // ���δ�ҵ������� -1
        return -1;
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

    // �ڿ���̨�д�ӡ����б�ĵ��Է���
    private void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    {
        // ��������б��е�ÿ�������Ʒ
        foreach (InventoryItem inventoryItem in inventoryList)
        {
            // ��ȡ��Ʒ��ϸ��Ϣ�������Ʒ����������������̨
            Debug.Log("Item Description:" + 
                InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode).itemDescription 
                + "    Item Quantity: " + inventoryItem.itemQuantity);
        }
        Debug.Log("************************************************************");
    }
}
