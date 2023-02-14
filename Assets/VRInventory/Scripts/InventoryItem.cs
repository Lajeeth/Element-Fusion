using UnityEngine;

namespace Epitybe.VRInventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "VR Inventory/Inventory Item")]
    public class InventoryItem : ScriptableObject
    {
        public GameObject displayPrefab;
        public GameObject gamePrefab;
    }
}
