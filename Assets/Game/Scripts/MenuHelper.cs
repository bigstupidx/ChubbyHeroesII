using UnityEngine;
using System.Collections;

public class MenuHelper : MonoBehaviour {

    public int restartFromGameplay = 0;

	void Awake () {
        DontDestroyOnLoad(gameObject);
	}

}
