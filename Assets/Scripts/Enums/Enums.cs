//仓库地点
// 表示不同库存位置的枚举类型
public enum InventoryLocation
{
    // 玩家库存位置
    player,

    // 宝箱库存位置（示例，可以根据需要添加更多位置）

    chest,

    // 用于计数，表示库存位置的总数
    count
}


public enum ToolEffect
{
    none,
    watering
}

public enum Direction
{
    up,
    down,
    left,
    right,
    none
}

public enum ItemType
{
    Seed,
    Commodity,
    Watering_tool,
    Hoeing_tool,
    Chopping_tool,
    Breaking_tool,
    Reaping_tool,
    Collecting_tool,
    Reapable_scenary,
    Furniture,
    none,
    count
}