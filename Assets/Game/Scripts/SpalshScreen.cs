using UnityEngine;
using UnityEngine.SceneManagement;

public class SpalshScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Invoke("loadMenuLevel",1.0f);
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Update is called once per frame
	Color c;
	void loadMenuLevel( )
	{

		SceneManager.LoadScene("Mainmenu");
	}
	 
	public GameObject logo;
	float alphaValue;
	void Update () {

		c = logo.GetComponent<Renderer>().material.color; 

		alphaValue = Mathf.Lerp (alphaValue, 255, 0.01f);
		c.a = alphaValue;
		logo.GetComponent<Renderer>().material.color = c;

		if(Input.GetKey(KeyCode.Mouse0) )
		{
			loadMenuLevel();
		}

		if(Input.GetKey(KeyCode.Escape) )
		{
			Application.Quit();
		}
	}
}
