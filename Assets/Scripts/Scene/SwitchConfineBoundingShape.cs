using Cinemachine;
using System;
using UnityEngine;

public class SwitchConfineBoundingShape : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SwitchBoundingShape();
    }

    /// <summary>
    /// �л�Collider�����������������Ļ�ı�Ե
    /// </summary>
    private void SwitchBoundingShape()
    {
        //�ҵ�polygon collider ��boundsconfiner ��Ϸ������
        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BoundsConfiner).GetComponent<PolygonCollider2D>();
        
        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();

        cinemachineConfiner.m_BoundingShape2D = polygonCollider2D; 

        //�������
        cinemachineConfiner.InvalidatePathCache();
    }
}
