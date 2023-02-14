using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Cloud;
    public GameObject Lava;
    public GameObject Air;
    public GameObject Sun;
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Water")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(Cloud, transform.position, transform.rotation);
        }

        if (other.tag == "Earth")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(Lava, transform.position, transform.rotation);
        }
        if (other.tag == "Air")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(Sun, transform.position, transform.rotation);
        }
    }
}
