
using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [ItemCodeDescription]
    [SerializeField]
    private int _itemCode;

    private SpriteRenderer spriteRenderer;

    public int ItemCode { get { return _itemCode; } set {  _itemCode = value; } }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if(ItemCode != 0)
        {
            Init(ItemCode);
        }
    }

    private void Init(int itemCode)
    {
        if(itemCode != 0)
        {
            // 设置物品代码
            ItemCode = itemCode;
            // 获取物品详细信息
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(ItemCode);
            // 设置物品的 Sprite
            spriteRenderer.sprite = itemDetails.itemSprite;
            // 如果物品类型为可飘动的场景物品
            if (itemDetails.itemType == ItemType.Reapable_scenary)
            {
                // 为物品游戏对象添加 ItemNudge 组件
                gameObject.AddComponent<ItemNudge>();
            }
        }
    }
}
