using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
	bool isObjDestoy = false;

	void OnBecameInvisible()
	{
		if (isObjDestoy)
        {
			Destroy (gameObject, 2f);
		}
	}

	void OnBecameVisible()
	{
		isObjDestoy = true;
	}
}
