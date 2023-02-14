using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject Obsidian;
    public GameObject Rain;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lava")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(Obsidian, transform.position, transform.rotation);
        }

        if (other.tag == "Air")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(Rain, transform.position, transform.rotation);
        }
    }
}
