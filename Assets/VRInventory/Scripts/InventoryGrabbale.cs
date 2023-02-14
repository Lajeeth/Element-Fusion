using System;
using UnityEngine;

namespace Epitybe.VRInventory
{
    public class InventoryGrabbale : OVRGrabbable
    {
        Storable storable;

        /// <summary>
        /// Notifies the object that it has been grabbed.
        /// </summary>
        public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            base.GrabBegin(hand, grabPoint);
            if (null != storable)
            {
                storable.SetGrabbed();
            }
        }

        /// <summary>
        /// Notifies the object that it has been released.
        /// </summary>
        public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(linearVelocity, angularVelocity);
            if (null != storable)
            {
                storable.SetReleased();
            }
        }

        void Awake()
        {
            if (m_grabPoints.Length == 0)
            {
                // Get the collider from the grabbable
                Collider collider = this.GetComponent<Collider>();
                if (collider == null)
                {
                    throw new ArgumentException("Grabbables cannot have zero grab points and no collider -- please add a grab point or collider.");
                }

                // Create a default grab point
                m_grabPoints = new Collider[1] { collider };
            }
            m_grabbedKinematic = GetComponent<Rigidbody>().isKinematic;
            storable = GetComponent<Storable>();
        }

        protected override void Start() { }

        void OnDestroy()
        {
            if (m_grabbedBy != null)
            {
                // Notify the hand to release destroyed grabbables
                m_grabbedBy.ForceRelease(this);
            }
        }
    }
}