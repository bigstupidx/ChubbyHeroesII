using UnityEngine;
using System.Collections;

public class TestForRayDraw : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	RaycastHit hit;
	// Update is called once per frame
	void Update () {
				Vector3 frwd = transform.TransformDirection (Vector3.right);
	
			Debug.DrawRay (transform.position,frwd , Color.red);
		if (Physics.Raycast (transform.position, frwd, out hit, 500)) {
						Debug.DrawRay (transform.position, Vector3.forward, Color.red);
						GameObject hitObj = hit.collider.gameObject;
						string hitObjName = hit.collider.name;
						string hitObjTagName = hit.collider.tag;
		
	
				}
		}
}
