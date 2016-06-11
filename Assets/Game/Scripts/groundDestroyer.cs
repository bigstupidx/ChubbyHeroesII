using UnityEngine;

public class groundDestroyer : MonoBehaviour
{
	//public bool canBeDestroyed = false ;
	Transform thisTrans;
    GameController CS_GameController;

    void Start()
    {
        thisTrans = transform;
        InvokeRepeating("CheckStatus", 2, 1.5f);
        CS_GameController = FindObjectOfType<GameController>() as GameController;
    }

    void CheckStatus()
    {
        //if (canBeDestroyed)
        //{
            if (PlayerController.thisPosition.z > thisTrans.localPosition.z + 160)
            {
                CS_GameController.ReturnPoolObject(gameObject);
            }
        //}
    }
}