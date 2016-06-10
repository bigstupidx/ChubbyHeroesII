using UnityEngine;
using System.Collections;

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
						Application.LoadLevel ("gameplay");
				justOnce = true;
		}
}
