
using System.Collections.Generic;
using UnityEngine;

// 创建一个新的 ScriptableObject 的右键菜单项
[CreateAssetMenu(fileName ="so_ItemList",menuName ="Scriptable Objects/Item/Item List")]
public class SO_ItemList : ScriptableObject
{
    // 序列化字段，用于存储项目详细信息的列表
    [SerializeField]
    public List<ItemDetails> itemDetails;

}
