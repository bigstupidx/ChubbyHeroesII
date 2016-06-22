using UnityEngine;

public class camTargetMovement : MonoBehaviour
{

    public Vector3 targetPosition;
    Transform thisTrans;
    public float speed ;

    void Start ()
    {
	    thisTrans = transform;
    }
	
// Update is called once per frame
    void Update ()
    {
	    thisTrans.localPosition = Vector3.MoveTowards( thisTrans.localPosition,targetPosition,speed*Time.deltaTime);

	    if (transform.localPosition == targetPosition)
            enabled = false;
    }
}
