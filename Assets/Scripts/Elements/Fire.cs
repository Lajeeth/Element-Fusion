using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Steam;
    public GameObject Lava;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            Instantiate(Steam, transform.position, transform.rotation);
        }

        if (other.tag == "Earth")
        {
            Instantiate(Lava, transform.position, transform.rotation);
        }
    }
}
