using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingRotation : MonoBehaviour
{

	public GameObject LoadinBg;
 		 
	void Start ()
	{
		Invoke ("StartGame", 3.0f);
	}

	//void Update ()
	//{
	//	LoadinBg.transform.Rotate (Vector3.forward * -10);
	//}
	void StartGame ()
	{
		SceneManager.LoadSceneAsync ("gamePlay");
	}
}
