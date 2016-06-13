using UnityEngine;

public class ObstacleMoving : MonoBehaviour {

	
	bool isVisible = false;
	
	void Update ()
    {
        if (isVisible)
        {
            transform.Translate (Vector3.forward * -10f * Time.deltaTime);
        }
    }

	void OnBecameVisible()
    {
		isVisible = true;
	}
	 
}
