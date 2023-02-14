using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Epitybe.VRInventory
{
    public class Storable : MonoBehaviour
    {
        Dictionary<InventoryContainer, int> m_candidates = new Dictionary<InventoryContainer, int>();
        public InventoryItem inventoryItem;
        bool isPreparing = false;
        bool isGrabbed = false;

        void LateUpdate()
        {
            ProcessInventoryStorage();
        }

        void OnTriggerEnter(Collider other)
        {
            InventoryContainer ic = other.GetComponent<InventoryContainer>();
            if (null == ic) return;

            // Check if current item in container is not equal to this inventory item
            if (null != ic.currentItem && ic.currentItem != inventoryItem)
            {
                return;
            }

            int refCount = 0;
            m_candidates.TryGetValue(ic, out refCount);
            m_candidates[ic] = refCount + 1;
        }

        void OnTriggerExit(Collider other)
        {
            InventoryContainer ic = other.GetComponent<InventoryContainer>();
            if (null == ic) return;

            int refCount = 0;
            bool found = m_candidates.TryGetValue(ic, out refCount);
            if (!found)
            {
                return;
            }

            if (refCount > 1)
            {
                m_candidates[ic] = refCount - 1;
            }
            else
            {
                m_candidates.Remove(ic);
            }

        }

        void ProcessInventoryStorage()
        {
            float closestMagSq = float.MaxValue;
            InventoryContainer closestContainer = null;

            foreach (InventoryContainer ic in m_candidates.Keys)
            {

                float magSq = (transform.position - ic.transform.position).sqrMagnitude;
                if (magSq < closestMagSq)
                {
                    closestMagSq = magSq;
                    closestContainer = ic;
                }
            }

            if (null != closestContainer)
            {
                if (isGrabbed)
                {
                    isPreparing = true;
                }
                else
                {
                    if (isPreparing)
                    {
                        closestContainer.Store(inventoryItem);
                        Destroy(gameObject);
                    }
                    isPreparing = false;
                }
            }
        }

        public void SetGrabbed() {
            isGrabbed = true;
        }

        public void SetReleased() {
            isGrabbed = false;
        }
    }
}
