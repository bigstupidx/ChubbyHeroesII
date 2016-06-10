using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

	// Use this for initialization
	public Texture[] CountDownTextures ;
	public GameController gameControllerScript;

	public GameObject hudParent;

	void Start () {
		gameControllerScript =  GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		tweenPunch ();
	}
	
	int i = 0 ;
	void tweenPunch()
	{
		transform.localScale = Vector3.one * 6;
		if (i >= 4) {
						 
						GetComponent<Renderer>().enabled = false;
				     	Destroy(gameObject,0.1f);
				} else {
						if (i == 3) {
				transform.localScale = Vector3.one * 3;
				GetComponent<Renderer>().material.mainTexture = CountDownTextures [i];
				//iTween.ScaleTo (gameObject, iTween.Hash ("scale", new Vector3 (4, 4, 4), "time", 1.0f, "easetype", iTween.EaseType.easeOutElastic,
				                                      //   "oncomplete", "tweenPunch"));
						} else {
								GetComponent<Renderer>().material.mainTexture = CountDownTextures [i];
				//iTween.ScaleTo (gameObject, iTween.Hash ("scale", new Vector3 (3, 3, 3), "time", 1.0f, "easetype", iTween.EaseType.easeOutElastic,
			                                                                      // "oncomplete", "tweenPunch"));

						}
				}

		i++;
	}

	void OnDisable()
	{
		hudParent.SetActive(true);
		gameControllerScript.ON_GAME_Start ();
	}
}
