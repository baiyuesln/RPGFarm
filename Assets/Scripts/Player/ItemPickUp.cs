
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    // 当进入2D触发器时调用的方法

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 获取碰撞对象上的 Item 组件
        Item item = collision.GetComponent<Item>();

        // 检查是否成功获取 Item 组件
        if (item != null)
        {
            // 获取与该物品相关的详细信息
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);

            //Debug.Log(itemDetails.itemDescription);
            // 检查物品是否可以被拾取
            if (itemDetails.canBePickedUp == true )
            {
                // 将物品添加到玩家库存，并销毁与之相关的游戏对象
                InventoryManager.Instance.AddItem(InventoryLocation.player, item, collision.gameObject);
            }
        }
    }

}
