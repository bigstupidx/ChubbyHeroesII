using UnityEngine;

public class takeScreenShot : MonoBehaviour
{
	bool takeScreenShots = false;
		// Use this for initialization
		void Start ()
		{

		}

		string resolution ;
		// Update is called once per frame
		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.G))
            {
			    Screen.SetResolution (640, 1136, false);
                resolution = "" + Screen.width + "X" + Screen.height;
			    Application.CaptureScreenshot ("ScreenShot-" + resolution + "-" + PlayerPrefs.GetInt ("number", 0) + ".png");
                PlayerPrefs.SetInt ("number", PlayerPrefs.GetInt ("number", 0) + 1);
			    //Debug.Log ("takenShot with " + resolution);

			}

	        if(Input.GetKeyDown(KeyCode.S))
	        {
		        takeScreenShots = true;
	        }
		
		    if(takeScreenShots)
		    {
			    InvokeRepeating("ScreeShorts",2.0f,1.0f);
		    }
		}

	void ScreeShorts()
	{
		Screen.SetResolution (640, 1136, false);
		resolution = "" + Screen.width + "X" + Screen.height;
		Application.CaptureScreenshot ("ScreenShot-" + resolution + "-" + PlayerPrefs.GetInt ("number", 0) + ".png");
		PlayerPrefs.SetInt ("number", PlayerPrefs.GetInt ("number", 0) + 1);
	}
}
