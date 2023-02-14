using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Epitybe.VRInventory
{
    public class InventorySlot : MonoBehaviour
    {
        [HideInInspector]
        public InventoryData inventoryData;
        public InventoryContainer inventoryContainer;
        public Text m_count;

        void Awake()
        {
            if (null == inventoryContainer)
            {
                Debug.LogError("Please assign Inventory Container");
            }
        }

        void Start()
        {
            UpdateSlot();
            inventoryContainer.OnItemAdded += AddItem;
            inventoryContainer.OnInventoryGrabbed += RemoveItem;
        }

        public void UpdateSlot()
        {
            // Update the container 
            if (null == inventoryData.item || 0 == inventoryData.count)
            {
                inventoryContainer.DestoryItem();
            }
            else
            {
                if (inventoryContainer.currentItem != inventoryData.item)
                {
                    inventoryContainer.DestoryItem();
                    inventoryContainer.InstantiateItem(inventoryData.item);
                }
            }
            // Update the text
            UpdateUI();
        }

        void AddItem(InventoryItem item)
        {
            inventoryData.item = item;
            inventoryData.count += 1;
            UpdateUI();
        }

        void RemoveItem()
        {
            if (null == inventoryData.item || 0 == inventoryData.count)
            {
                return;
            }

            inventoryData.count -= 1;
            if (0 == inventoryData.count)
            {
                inventoryData.item = null;
                inventoryContainer.DestoryItem();
            }
            UpdateUI();
        }

        void UpdateUI()
        {
            if (inventoryData.count <= 0)
            {
                m_count.gameObject.SetActive(false);
            }
            else
            {
                m_count.text = inventoryData.count.ToString();

                if (!m_count.gameObject.activeSelf)
                {
                    m_count.gameObject.SetActive(true);
                }
            }
        }
    }
}