using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenu : MonoBehaviour
{
    bool justOnce = false;

    void Start ()
	{
		Invoke ("PlayGame", 1.0f);
	}
	
	void PlayGame ()
	{
		if (!justOnce)
				SceneManager.LoadScene ("gameplay");
		justOnce = true;
	}
}
