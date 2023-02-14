using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public GameObject Sand;
    public GameObject Mud;
    public GameObject Plant;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Air")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(Sand, transform.position, transform.rotation);
        }
        if (other.tag == "Water")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(Mud, transform.position, transform.rotation);
        }
        if (other.tag == "Sun")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(Plant, transform.position, transform.rotation);
        }

    }
}
