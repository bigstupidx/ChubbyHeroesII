using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	bool isObjDestoy = false;

	void OnBecameInvisible()
	{
		if (isObjDestoy)
        {
			Destroy (gameObject, 3f);
		}
	}

	void OnBecameVisible()
	{
		isObjDestoy = true;
	}
}
