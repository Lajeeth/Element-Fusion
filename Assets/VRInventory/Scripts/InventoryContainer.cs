using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epitybe.VRInventory
{
    public class InventoryContainer : MonoBehaviour
    {
        public delegate void OnItemAddedHandler(InventoryItem item);
        public event OnItemAddedHandler OnItemAdded;
        public delegate void OnInventoryGrabbedHandler();
        public event OnInventoryGrabbedHandler OnInventoryGrabbed;
        [HideInInspector]
        public InventoryItem currentItem;
        GameObject m_currentDisplay;

        public void Store(InventoryItem item)
        {
            if (null == currentItem)
            {
                InstantiateItem(item);
                OnItemAdded.Invoke(item);
                return;
            }

            if (currentItem != item)
            {
                return;
            }
            else
            {
                if (null != OnItemAdded)
                {
                    OnItemAdded.Invoke(item);
                }
            }
        }

        public void InstantiateItem(InventoryItem item)
        {
            m_currentDisplay = Instantiate(item.displayPrefab, transform);
            currentItem = item;
        }

        public void DestoryItem()
        {
            if (null != m_currentDisplay)
            {
                Destroy(m_currentDisplay);
            }
            currentItem = null;
        }

        public void RemoveItem()
        {
            if (null != OnInventoryGrabbed)
            {
                OnInventoryGrabbed.Invoke();
            }
        }
    }
}
