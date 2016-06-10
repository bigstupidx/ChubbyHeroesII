using UnityEngine;
using System.Collections;

public class SameButtonTextures : MonoBehaviour {

	// Use this for initialization

	public Texture SameButtonDown,SameButtonUp;
	public static SameButtonTextures Static ;
	void OnEnable () {
	
		Static = this;
	}
	
 
}
