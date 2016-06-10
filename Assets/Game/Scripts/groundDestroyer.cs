using UnityEngine;

public class groundDestroyer : MonoBehaviour
{
	public bool canBeDestroyed = false ;
	Transform thisTrans;

    void Start()
    {
        thisTrans = transform;
        InvokeRepeating("CheckStatus", 2, 1.5f);
    }

    void CheckStatus()
    {
        if (canBeDestroyed)
        {
            if (PlayerController.thisPosition.z > thisTrans.localPosition.z + 160)
            {
                Destroy(gameObject);
            }
        }
    }
}