using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 70f);
    }
}
