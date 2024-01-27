
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
            // ������Ʒ����
            ItemCode = itemCode;
            // ��ȡ��Ʒ��ϸ��Ϣ
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(ItemCode);
            // ������Ʒ�� Sprite
            spriteRenderer.sprite = itemDetails.itemSprite;
            // �����Ʒ����Ϊ��Ʈ���ĳ�����Ʒ
            if (itemDetails.itemType == ItemType.Reapable_scenary)
            {
                // Ϊ��Ʒ��Ϸ������� ItemNudge ���
                gameObject.AddComponent<ItemNudge>();
            }
        }
    }
}
