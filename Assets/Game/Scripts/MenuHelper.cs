using UnityEngine;
using System.Collections;

public class MenuHelper : MonoBehaviour {

    public static MenuHelper _Instance;

    public int restartFromGameplay = 0;

	void Awake () {

        if (_Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _Instance = this;
        }

        DontDestroyOnLoad(gameObject);
	}

}
