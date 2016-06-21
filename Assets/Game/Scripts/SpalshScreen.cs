using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpalshScreen : MonoBehaviour {

	void Start ()
    {
		Invoke("LoadGame",2.0f);
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

    void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
