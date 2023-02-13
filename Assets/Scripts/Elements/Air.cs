using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    public GameObject Cloud;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Steam")
        {
            Instantiate(Cloud, transform.position, transform.rotation);
        }
    }
}
