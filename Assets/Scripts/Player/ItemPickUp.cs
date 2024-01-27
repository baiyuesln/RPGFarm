
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    // ������2D������ʱ���õķ���

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ȡ��ײ�����ϵ� Item ���
        Item item = collision.GetComponent<Item>();

        // ����Ƿ�ɹ���ȡ Item ���
        if (item != null)
        {
            // ��ȡ�����Ʒ��ص���ϸ��Ϣ
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);

            //Debug.Log(itemDetails.itemDescription);
            // �����Ʒ�Ƿ���Ա�ʰȡ
            if (itemDetails.canBePickedUp == true )
            {
                // ����Ʒ��ӵ���ҿ�棬��������֮��ص���Ϸ����
                InventoryManager.Instance.AddItem(InventoryLocation.player, item, collision.gameObject);
            }
        }
    }

}
