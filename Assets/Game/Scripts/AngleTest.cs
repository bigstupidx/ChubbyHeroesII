using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AngleTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public Text angleTxt;
	// Update is called once per frame
	Vector2 MouseStartPos;
	public Transform start,end;
	void Update () {

	 	 
		angleTxt.text = ""+	Mathf.Atan2(start.position.x, end.position.x) * Mathf.Rad2Deg;
			
		 
	
	}
}
