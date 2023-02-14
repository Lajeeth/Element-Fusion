using UnityEngine;
using UnityEngine.XR;

namespace Epitybe.VRInventory.Demo
{
    public class DemoRecenter : MonoBehaviour
    {
        public OVRInput.Button recenterButton = OVRInput.Button.Two;
        public OVRInput.Controller controller;

        void LateUpdate()
        {

            if (OVRInput.GetDown(recenterButton, controller))
            {
                InputTracking.Recenter();
            }
        }
    }
}
