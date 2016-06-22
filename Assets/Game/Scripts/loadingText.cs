using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loadingText : MonoBehaviour {

	public Text loading;
	public string[] TextArray ;
	float index;
	float speed=5;
	
	void Update ()
    {
		loading.text = TextArray[ Mathf.RoundToInt(index)];
		index+= speed*Time.deltaTime;
		if(index > TextArray.Length-1) index = 0;
	}
}
