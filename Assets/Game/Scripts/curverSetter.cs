using UnityEngine;


public class curverSetter : MonoBehaviour
{
	public Vector4 curveOffsetValues;
	// Update is called once per frame

	public Vector4 targetCurveVector;
    private Vector4 lastKnownOffsetValue;


    void Start ()
	{
        NoBendJustRunStraight();
	}

 
	void Update ()
	{
		curveOffsetValues = Vector4.MoveTowards (curveOffsetValues, targetCurveVector, 0.005f);

		Shader.SetGlobalVector ("_Offset", curveOffsetValues);
 
	}

	void OnDisable ()
	{
        lastKnownOffsetValue = curveOffsetValues;

        Shader.SetGlobalVector ("_Offset", lastKnownOffsetValue); 
	}

    // first start straight
    void NoBendJustRunStraight()
    {
        targetCurveVector = new Vector4(0, -5, 0);
        Invoke("BendToRightCurve", 6f);
    }


    //cycling through left and right curves 

    public void BendToRightCurve ()
	{
		targetCurveVector = new Vector4 (12, -5, 0);
		Invoke ("BendToLeftCurve", 30);
	} 

	public void BendToLeftCurve ()
	{
		targetCurveVector = new Vector4 (-12, -5, 0);
		Invoke ("BendToRightCurve", 30);
	}
}
