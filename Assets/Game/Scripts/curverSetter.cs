using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class curverSetter : MonoBehaviour
{

 
		public Vector4 curveOffsetValues;
		// Update is called once per frame

		public Vector4 targetCurveVector;
	 
		void Start ()
		{

	 
				BendToRightCurve ();
		 

		}

 
		void Update ()
		{

	
				curveOffsetValues = Vector4.MoveTowards (curveOffsetValues, targetCurveVector, 0.04f);


				Shader.SetGlobalVector ("_Offset", curveOffsetValues);
 
	
		}

		void OnDisable ()
		{
				Shader.SetGlobalVector ("_Offset", Vector4.zero);
		 
		}


		//cycling through left and right curves 

		public void BendToRightCurve ()
		{
		
				targetCurveVector = new Vector4 (18, -12, 0);
				Invoke ("BendToLeftCurve", 30);
		} 
		public void BendToLeftCurve ()
		{
		
				targetCurveVector = new Vector4 (-18, -12, 0);
				Invoke ("BendToRightCurve", 30);
		}
}
