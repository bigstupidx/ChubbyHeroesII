using UnityEngine;
using System.Collections;

public class LoadingRotation : MonoBehaviour
{

		// Use this for initialization

		public GameObject LoadinBg;
 
		 
		void Start ()
		{
				Invoke ("StartGame", 3.0f);
		}

		void Update ()
		{
				LoadinBg.transform.Rotate (Vector3.forward * -10);
				//Debug.Log("game Started here");
				


		}
		void StartGame ()
		{
				Application.LoadLevelAsync ("gamePlay");
		}

 


}
