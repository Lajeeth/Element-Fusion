using System.Collections.Generic;

namespace Epitybe.VRInventory
{
    [System.Serializable]
    public class InventoryData
    {
        public InventoryItem item;
        public int count;
    }

    [System.Serializable]
    // InventoryDataStore is a helper class to serialize a list of inventory data
    public class InventoryDataStore
    {
        public List<InventoryData> inventoryData;
    }
}