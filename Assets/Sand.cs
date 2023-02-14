using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : MonoBehaviour
{
    public GameObject Dustcloud;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cloud")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(Dustcloud, transform.position, transform.rotation);
        }
    }
}
