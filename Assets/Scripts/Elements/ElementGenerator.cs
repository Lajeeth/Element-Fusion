using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementGenerator : MonoBehaviour
{
    public GameObject Fire;
    public GameObject Water;
    
    private void OnTriggerEnter(Collider other)
    {

        Vector3 otherPos = other.transform.position;
        otherPos.x = (float)(otherPos.x + 1.2);
        otherPos.y = (float)(otherPos.x + 1.2);
        otherPos.z = (float)(otherPos.x + 1.2);

        if (other.tag == "Water")
        {
            Destroy(other.gameObject);
            Instantiate(Water, otherPos, transform.rotation);
            Instantiate(Water, otherPos, transform.rotation);
        }
        else if(other.tag == "Fire")
        {
            Destroy(other.gameObject);
            Instantiate(Fire, transform.position, transform.rotation);
        }
    }
}
