using UnityEngine;
using System.Collections;

public class ShurikenController : MonoBehaviour
{


		public float speed ;
		public static Vector3 brokenBarrrel, brokenPot;
		public static bool isShurikenTouches = false;
		Transform thisTransform;
		public GameObject particalEffect;

		public Vector3 rayOffset ;
		Vector3 rotationDirection;
		void Start ()
		{
				thisTransform = transform;
 
				rotationDirection = new Vector3 (0, 0, 60);
			 
				Destroy (gameObject, 2);

		}

		void Update ()
		{

				// Translate forward direction with Speed variable 

				thisTransform.Translate (Vector3.forward * speed * Time.deltaTime);
				//	thisTransform.Rotate (rotationDirection, Space.Self);
				checkCollisionWithRay ();
		}
		RaycastHit hit ;
		public void  checkCollisionWithRay ()
		{
		
		
	 
				if (Physics.Raycast (thisTransform.position + rayOffset, Vector3.forward * 2, out hit, 5)) {



						OnTriggerEnterWithRay (hit.collider);
				}
	 
		
		}

		// This Game Object Trigger with barrel or pot

		void OnTriggerEnterWithRay (Collider incoming)
		{
				//Debug.Log("Shriken TriggerWith............" + incoming.name);

				string incomingTag = incoming.tag;
				GameObject incomingObj = incoming.gameObject;

				if (incomingTag.Contains ("Barrel")) { 
						brokenBarrrel = incomingObj.transform.position;
						GameController.Static.GenerateBrokenBarrel ();
						SoundController.Static.playSoundFromName ("Pot");
						Destroy (incomingObj); // destroy  barrel or pot
						Destroy (gameObject); // destroy Shuriken
						PlayerPrefs.SetInt ("DailyMissionDestroyBarrelCount", PlayerPrefs.GetInt ("DailyMissionDestroyBarrelCount") - 1);
						isShurikenTouches = true;
				} else if (incomingTag.Contains ("Pots")) {   
						brokenPot = incomingObj.transform.position;
						GameController.Static.GenerateBrokenPots ();
						SoundController.Static.playSoundFromName ("Pot");
						Destroy (incomingObj); // destroy  barrel or pot
						Destroy (gameObject); // destroy Shuriken
						PlayerPrefs.SetInt ("DailyMissionDestroyPotsCount", PlayerPrefs.GetInt ("DailyMissionDestroyPotsCount") - 1);
						isShurikenTouches = true;
				}

		}
		void OnCollisionEnter (Collision incoming)
		{
				string incomingTag = incoming.collider.tag;
				GameObject incomingObj = incoming.gameObject;
				if (incomingTag.Contains ("Obstacle")) {
						Debug.Log ("Shriken touches");
						GameObject obj = Instantiate (particalEffect, gameObject.transform.position + new Vector3 (0, 0, -2.5f),
			                             Quaternion.identity)as GameObject;
						Destroy (obj, 0.5f);
						Destroy (gameObject);

						if (incoming.collider.name.Contains ("Sword") && (incoming.rigidbody == null)) {
								incoming.collider.transform.parent = null;
								incoming.gameObject.AddComponent<Rigidbody> ().AddForce (Vector3.forward * 3800);
								GetComponent<Collider>().enabled = false;
								Destroy (incomingObj, 3.0f);
				 
						}
				}

		}

		void OnBecameInvisible ()
		{
				//Debug.Log("Shuriken Destroyed");
				Destroy (gameObject);
		}

}
