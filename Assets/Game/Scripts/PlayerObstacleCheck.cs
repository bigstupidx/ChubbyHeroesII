using UnityEngine;
using System.Collections;

public class PlayerObstacleCheck : MonoBehaviour
{

	Transform thisTrans;
	RaycastHit hit;
	string hitObjName;
	public   bool ObstacleAtRightSide, ObstacleAtLeftSide;
	public Vector3 charPositionOffset ;
	void Start ()
	{
		thisTrans = transform;
	}
	

	public bool CheckRightSide ()
	{
		Debug.DrawRay (thisTrans.position, Vector3.right * 2, Color.red, 2, false);
		 
		if (Physics.Raycast (transform.position + charPositionOffset, Vector3.right * 2, out hit, 8)) {
			GameObject hitObj = hit.collider.gameObject;
			
			if (hitObj.tag.Contains ("Obstacle")) {
				ObstacleAtRightSide = true;
				return true;
			} else {
				ObstacleAtRightSide = false;
				return false;
			}

		} else
			return false;

	}
	public bool CheckLeftSide ()
	{
		   

		Debug.DrawRay (thisTrans.position + charPositionOffset, Vector3.left * 2, Color.blue, 2, false);

		 
		if (Physics.Raycast (transform.position, Vector3.left * 2, out hit, 8)) {
			
			GameObject hitObj = hit.collider.gameObject;
			if (hitObj.tag.Contains ("Obstacle")) {
				ObstacleAtLeftSide = true;
				return true;
			} else {
				ObstacleAtLeftSide = false;
				return false;
			}
			
		} else
			return false;

		 
	}
}
