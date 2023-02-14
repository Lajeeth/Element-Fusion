using UnityEngine;

namespace Epitybe.VRInventory.Demo
{
    public class InventoryDisplayControl : MonoBehaviour
    {
        public OVRInput.Button displayButton = OVRInput.Button.One;
        public OVRInput.Controller controller;
        public GameObject inventory;
        public float distanceToCamera = 0.5f;

        Transform cameraRig;

        void Start()
        {
            cameraRig = OVRManager.instance.GetComponent<OVRCameraRig>().centerEyeAnchor;
        }

        void LateUpdate()
        {
            if (null == inventory)
            {
                return;
            }

            if (OVRInput.GetDown(displayButton, controller))
            {
                ToggleObject(inventory);
                if (inventory.activeSelf)
                {
                    inventory.transform.position =
                        cameraRig.position + Camera.main.transform.forward * distanceToCamera;
                    inventory.transform.LookAt(cameraRig);
                    inventory.transform.Rotate(0f, 180f, 0f);
                }
            }

        }

        void ToggleObject(GameObject go)
        {
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
            else
            {
                go.SetActive(true);
            }
        }
    }
}
