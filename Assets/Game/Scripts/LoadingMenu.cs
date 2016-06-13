using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenu : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				Invoke ("PlayGame", 1.0f);
		}
	
		 

		bool justOnce = false;
		void PlayGame ()
		{
				if (!justOnce)
						SceneManager.LoadScene ("gameplay");
				justOnce = true;
		}
}
