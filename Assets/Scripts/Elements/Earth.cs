using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public GameObject Dust;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Air")
        {
            Instantiate(Dust, transform.position, transform.rotation);
        }
    }
}
