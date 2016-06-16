using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public GameObject target;
    Vector3 targetPos;

	// Use this for initialization
	void Start () {
        
        Invoke("SelfDetruct", 2f);
    }
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            targetPos = target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 200f);
        }
        else
        SelfDetruct();
    }

    void SelfDetruct()
    {
        Destroy(gameObject);
    }
}
