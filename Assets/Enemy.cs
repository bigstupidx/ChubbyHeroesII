using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
