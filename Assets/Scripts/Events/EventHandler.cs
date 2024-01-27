using System;
using System.Collections.Generic;


public delegate void MovementDelegate(float inputX, float inputY,bool isWalking ,bool isRunning,bool isIdle,
    bool isCarrying,ToolEffect toolEffect,
    bool isUsingToolRight,bool isUsingToolLeft,bool isUsingToolUp,bool isUsingToolDown,
    bool isLiftingToolRight,bool isLiftingToolLeft,bool isLiftingToolUp,bool isLiftingToolDown,
    bool isPickingRight,bool isPickingLeft,bool isPickingUp,bool isPickingDown,
    bool isSwingingToolRight,bool isSwingingToolLeft,bool isSwingingToolUp,bool isSwingingToolDown,
    bool idleUp,bool idleDown,bool idleLeft,bool idleRight);
public static class EventHandler
{
    //// 库存更新事件，当库存发生变化时触发
    public static event Action<InventoryLocation, List<InventoryItem>> InventoryUpdatedEvent;

    // 调用库存更新事件的回调方法
    public static void CallInventoryUpdatedEvent(InventoryLocation inventoryLocation, List<InventoryItem> items)
    {
        // 检查是否有订阅库存更新事件的监听者
        if (InventoryUpdatedEvent != null)
        {
            // 调用库存更新事件并传递库存位置和库存物品列表
            InventoryUpdatedEvent(inventoryLocation, items);
        }
    }

    //声明movement委托事件
    public static event MovementDelegate MovementEvent;

    //发布者回调事件 移动动画
    public static void CallMovementEvent(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle,
    bool isCarrying, ToolEffect toolEffect,
    bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
    {
        if (MovementEvent != null)
            MovementEvent(inputX, inputY, isWalking, isRunning, isIdle,
                isCarrying, toolEffect,
                isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
                isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
                isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
                isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
                idleUp, idleDown, idleLeft, idleRight);
    }
}
