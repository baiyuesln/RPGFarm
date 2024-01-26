using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System;

// ����һ���Զ������Ի������������ض����ԣ�ItemCodeDescriptionAttribute��
[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    // ��д������ָ���ڼ����������Եĸ߶�
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // ��Ĭ�����Եĸ߶ȼӱ����Ա�Ϊ���������ڳ��ռ�
        return EditorGUI.GetPropertyHeight(property) * 2;
    }

    // ��д�������ڼ������л������Ե��Զ��� GUI
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // ��ʼ���Դ�����ȷ����ȷ�� GUI ��Ⱦ
        EditorGUI.BeginProperty(position, label, property);

        // ������������Ƿ�Ϊ����
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            // ��ʼ�������ֵ�ĸ���
            EditorGUI.BeginChangeCheck();

            // ���������ֶΣ�����ȡ�µ�ֵ
            var newValue = EditorGUI.IntField(new Rect(position.x, position.y,
            position.width, position.height / 2), label, property.intValue);

            // ��ʾ��Ŀ������ǩ�Ͷ�Ӧ����Ŀ�����ı�
            EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2,
            position.width, position.height / 2), "Item Description", GetItemDescription(property.intValue));

            // �������ֵ�������ģ���������ֵ
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = newValue;
            }
        }
        // �������Դ���
        EditorGUI.EndProperty() ;

    }

    // ��������ֵ��ȡ��Ӧ����Ŀ����
    private string GetItemDescription(int intValue)
    {
        // ���ذ�����Ŀ�б�� ScriptableObject
        SO_ItemList so_itemList;
        so_itemList = AssetDatabase.LoadAssetAtPath("Assets/Scriptable Object Assets/Item/so_ItemList.asset", typeof(SO_ItemList)) as SO_ItemList;

        // ��ȡ��Ŀ�����б�
        List<ItemDetails> itemDetails = so_itemList.itemDetails;

        // ��������ֵ����ƥ�����Ŀ����
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
