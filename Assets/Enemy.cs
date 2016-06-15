using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public bool targetable = true;
    public GameObject targetedNotifier;

    void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                Destroy(other.gameObject);
            }
        }

    public void Targeted()
    {
        targetedNotifier.SetActive(true);
    }
}
