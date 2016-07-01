using UnityEngine;

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
		if (i >= 4)
        {	 
			GetComponent<Renderer>().enabled = false;
			Destroy(gameObject,0.1f);
		}
        else
        {
			if (i == 3)
            {
				transform.localScale = Vector3.one * 3;
				GetComponent<Renderer>().material.mainTexture = CountDownTextures [i];
			}
            else
            {
				GetComponent<Renderer>().material.mainTexture = CountDownTextures [i];
			}
	    }

		i++;
	}

	//void OnDisable()
	//{
	//	hudParent.SetActive(true);
	//	gameControllerScript.OnGameStart ();
	//}
}
