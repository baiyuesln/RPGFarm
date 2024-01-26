using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System;

// 定义一个自定义属性绘制器，用于特定属性（ItemCodeDescriptionAttribute）
[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    // 重写方法以指定在检视器中属性的高度
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 将默认属性的高度加倍，以便为额外内容腾出空间
        return EditorGUI.GetPropertyHeight(property) * 2;
    }

    // 重写方法以在检视器中绘制属性的自定义 GUI
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 开始属性处理，以确保正确的 GUI 渲染
        EditorGUI.BeginProperty(position, label, property);

        // 检查属性类型是否为整数
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            // 开始检查属性值的更改
            EditorGUI.BeginChangeCheck();

            // 绘制整数字段，并获取新的值
            var newValue = EditorGUI.IntField(new Rect(position.x, position.y,
            position.width, position.height / 2), label, property.intValue);

            // 显示项目描述标签和对应的项目描述文本
            EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2,
            position.width, position.height / 2), "Item Description", GetItemDescription(property.intValue));

            // 如果属性值发生更改，更新属性值
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = newValue;
            }
        }
        // 结束属性处理
        EditorGUI.EndProperty() ;

    }

    // 根据整数值获取对应的项目描述
    private string GetItemDescription(int intValue)
    {
        // 加载包含项目列表的 ScriptableObject
        SO_ItemList so_itemList;
        so_itemList = AssetDatabase.LoadAssetAtPath("Assets/Scriptable Object Assets/Item/so_ItemList.asset", typeof(SO_ItemList)) as SO_ItemList;

        // 获取项目详情列表
        List<ItemDetails> itemDetails = so_itemList.itemDetails;

        // 根据整数值查找匹配的项目详情
        ItemDetails itemDetail = itemDetails.Find(x => x.itemCode == intValue);

        if(itemDetail != null)
        {
            return itemDetail.itemDescription;
        }
        else
        {
            return "";
        }
    }
}
