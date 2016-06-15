using UnityEngine;

public class ObstacleMoving : MonoBehaviour {

	bool isVisible = false;
    public float moveSpeed = 10f;

	void Update ()
    {
        if (isVisible)
        {
            transform.Translate (Vector3.forward * -moveSpeed * Time.deltaTime);
        }
    }

	void OnBecameVisible()
    {
        isVisible = true;
	}
}
