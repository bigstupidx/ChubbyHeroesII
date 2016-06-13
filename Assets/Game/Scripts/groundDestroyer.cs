using UnityEngine;

public class groundDestroyer : MonoBehaviour
{
    public bool isInPool = false;
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
        if (!isInPool)
        {
            if (PlayerController.thisPosition.z > thisTrans.localPosition.z + 160)
            {
                CS_GameController.ReturnPoolObject(gameObject);
            }
        }
    }
}