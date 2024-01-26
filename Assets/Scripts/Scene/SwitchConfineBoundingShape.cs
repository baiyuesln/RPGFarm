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
    /// 切换Collider用来限制相机定义屏幕的边缘
    /// </summary>
    private void SwitchBoundingShape()
    {
        //找到polygon collider 在boundsconfiner 游戏物体上
        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BoundsConfiner).GetComponent<PolygonCollider2D>();
        
        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();

        cinemachineConfiner.m_BoundingShape2D = polygonCollider2D; 

        //清除缓存
        cinemachineConfiner.InvalidatePathCache();
    }
}
