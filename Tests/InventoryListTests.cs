using Core;
using Core.Interfaces;

namespace Tests;

public class DummyInventoryItem : IInventory
{
    public int Number { get; set; } = 0;
    public override string ToString() => $"DummyItem (№{Number})";
}

public class InventoryListTests
{
    [Fact]
    public void InventoryList_AutoAssignNumber_WhenNumberNotSet()
    {
        // Arrange
        var inventoryList = new InventoryList();
        var dummyItem = new DummyInventoryItem(); // Number = 0

        // Act
        inventoryList.Add(dummyItem);

        // Assert – номер должен быть автоматически присвоен
        Assert.NotEqual(0, dummyItem.Number);
    }

    [Fact]
    public void InventoryList_AssignsUniqueNumbers_ForMultipleItems()
    {
        // Arrange
        var inventoryList = new InventoryList();
        var item1 = new DummyInventoryItem();
        var item2 = new DummyInventoryItem();

        // Act
        inventoryList.Add(item1);
        inventoryList.Add(item2);

        // Assert – номера должны быть разными
        Assert.NotEqual(item1.Number, item2.Number);
    }

    [Fact]
    public void InventoryList_DoesNotOverride_PreAssignedNumber()
    {
        // Arrange
        var inventoryList = new InventoryList();
        var item = new DummyInventoryItem { Number = 99 };

        // Act
        inventoryList.Add(item);

        // Assert – если номер уже задан, он не меняется
        Assert.Equal(99, item.Number);
    }
}