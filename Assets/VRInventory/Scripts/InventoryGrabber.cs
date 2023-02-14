using UnityEngine;
using System.Collections.Generic;

namespace Epitybe.VRInventory
{
    public class InventoryGrabber : OVRGrabber
    {
        Dictionary<InventoryContainer, int> m_candidates = new Dictionary<InventoryContainer, int>();
        // Support variables
        bool triggerPressed = false;
        bool triggerPressedLastFrame = false;

        bool TriggerReleasedThisFrame()
        {
            return (triggerPressedLastFrame && !triggerPressed);
        }

        bool TriggerPressedThisFrame()
        {
            return (!triggerPressedLastFrame && triggerPressed);
        }

        public override void Update()
        {
            base.Update();
            triggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, m_controller);
            ErrorChecking();
            ProcessInventoryFetching();
            triggerPressedLastFrame = triggerPressed;
        }

        void ProcessInventoryFetching()
        {
            if (TriggerPressedThisFrame())
            {
                InventoryContainer candidate = SearchClosetContainer();
                if (null != candidate && null != candidate.currentItem)
                {
                    GameObject go = Instantiate(
                        candidate.currentItem.gamePrefab,
                        transform.position,
                        transform.rotation
                    );
                    candidate.RemoveItem();
                    m_candidates.Clear();

                    OVRGrabbable grabbable = go.transform.GetComponent<OVRGrabbable>();
                    if (grabbable == null) return;

                    // Add the grabbable
                    int refCount = 0;
                    m_grabCandidates.TryGetValue(grabbable, out refCount);
                    m_grabCandidates[grabbable] = refCount + 1;
                    GrabBegin();
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (null != other.GetComponent<InventoryContainer>())
            {
                AddContainerCandidate(other.GetComponent<InventoryContainer>());
            }

            // Get the grab trigger
            OVRGrabbable grabbable = other.GetComponent<OVRGrabbable>() ?? other.GetComponentInParent<OVRGrabbable>();
            if (grabbable == null) return;

            // Add the grabbable
            int refCount = 0;
            m_grabCandidates.TryGetValue(grabbable, out refCount);
            m_grabCandidates[grabbable] = refCount + 1;
        }

        void OnTriggerExit(Collider other)
        {
            if (null != other.GetComponent<InventoryContainer>())
            {
                RemoveContainerCandidate(other.GetComponent<InventoryContainer>());
            }

            OVRGrabbable grabbable = other.GetComponent<OVRGrabbable>() ?? other.GetComponentInParent<OVRGrabbable>();
            if (grabbable == null) return;

            // Remove the grabbable
            int refCount = 0;
            bool found = m_grabCandidates.TryGetValue(grabbable, out refCount);
            if (!found)
            {
                return;
            }

            if (refCount > 1)
            {
                m_grabCandidates[grabbable] = refCount - 1;
            }
            else
            {
                m_grabCandidates.Remove(grabbable);
            }
        }

        void AddContainerCandidate(InventoryContainer ic)
        {
            if (null == ic) return;

            int refCount = 0;
            m_candidates.TryGetValue(ic, out refCount);
            m_candidates[ic] = refCount + 1;
        }

        void RemoveContainerCandidate(InventoryContainer ic)
        {
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

        InventoryContainer SearchClosetContainer()
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
            return closestContainer;
        }

        void ErrorChecking()
        {
            if (m_grabCandidates.Count == 0)
            {
                return;
            }

            List<OVRGrabbable> removals = new List<OVRGrabbable>();

            // Iterate grab candidates and find the closest grabbable candidate
            foreach (OVRGrabbable grabbable in m_grabCandidates.Keys)
            {
                bool canGrab = !(grabbable.isGrabbed && !grabbable.allowOffhandGrab);
                if (!canGrab)
                {
                    continue;
                }

                for (int j = 0; j < grabbable.grabPoints.Length; ++j)
                {
                    Collider grabbableCollider = grabbable.grabPoints[j];
                    if (null == grabbableCollider)
                    {
                        removals.Add(grabbable);
                        break;
                    }
                }
            }

            foreach (OVRGrabbable grabbable in removals)
            {
                m_grabCandidates.Remove(grabbable);
            }
        }
    }
}