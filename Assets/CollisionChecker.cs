using UnityEngine;
using System.Collections;

public class CollisionChecker : MonoBehaviour
{
    PlayerController playerControllerScript;

    public bool turnOnce;

    void Start()
    {
        playerControllerScript = GetComponentInParent<PlayerController>();
    }


    void OnTriggerEnter(Collider other)
    {
        GameObject incomingObj = other.gameObject;

        if (other.CompareTag("EnterIn"))
        {
            // remember from where I enetered in the intersection
            switch (incomingObj.name)
            {
                case "NORTH":
                    playerControllerScript._currentTurnCollider = PlayerController.TurnColliders.NORTH;
                    break;
                case "EAST":
                    playerControllerScript._currentTurnCollider = PlayerController.TurnColliders.EAST;
                    break;
                case "SOUTH":
                    playerControllerScript._currentTurnCollider = PlayerController.TurnColliders.SOUTH;
                    break;
                case "WEST":
                    playerControllerScript._currentTurnCollider = PlayerController.TurnColliders.WEST;
                    break;
                default:
                    break;
            }
            playerControllerScript.IntersectionParent = other.transform.parent;
 
        }

        if(other.CompareTag("InterZone"))
        {
            turnOnce = true;
            Debug.Log("turnOnce " + turnOnce);
            // enable turn buttons and turn logic

            // get intersection type
            switch (incomingObj.name)
            {
                case "IntersectionX":
                    playerControllerScript.intersectionType = PlayerController.IntersectionType.x;
                    break;
                case "IntersectionY":
                    playerControllerScript.intersectionType = PlayerController.IntersectionType.y;
                    break;
                case "IntersectionT":
                    playerControllerScript.intersectionType = PlayerController.IntersectionType.t;
                    break;
                default:
                    break;
            }
        }

        if (other.CompareTag("Turn"))
        {
            Debug.Log("turnOnce is: " + turnOnce);
            if (turnOnce && (InputController.Static.turnSide != InputController.TurnSide.none))
            {
                Debug.Log("trigger small turn collider");
                // perform turning based on where i entered
                playerControllerScript.currentTurnLaneBlock = other.transform;
                playerControllerScript.GetNewStreetTarget(playerControllerScript.intersectionType);
                
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Turn"))
        {
        }

    }
}
