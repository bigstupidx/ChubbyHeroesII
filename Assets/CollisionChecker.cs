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

    int colNumber = 0;
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

        if(other.CompareTag("InterZone")) // aici tre sa inregistrez swipe de turn si sa fac disable la turnuri in functie de ce lane am
        {
            turnOnce = true;
            //playerControllerScript.canChangeLane = false;
            //playerControllerScript.canTurn = true;
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
            colNumber++;
            
            if (turnOnce && (InputController.Static.turnSide == InputController.TurnSide.left))
            {

                switch (playerControllerScript.currentLane)
                {           
                    case PlayerController.PlayerLane.one:
                        if (colNumber == 1)
                        {
                            CallTurning(other);
                        }
                        break;
                    case PlayerController.PlayerLane.two:                   
                        if(colNumber == 2)
                        {
                            CallTurning(other);
                        }
                        break;
                    case PlayerController.PlayerLane.three:

                        if (colNumber == 3)
                        {
                            CallTurning(other);
                        }
                        break;
                    case PlayerController.PlayerLane.four:

                        if (colNumber == 4)
                        {
                            CallTurning(other);
                        }
                        break;
                    default:
                        break;
                }

                //Debug.Log(colNumber);
            }

            if (turnOnce && (InputController.Static.turnSide == InputController.TurnSide.right))
            {
                
                switch (playerControllerScript.currentLane)
                {
                    case PlayerController.PlayerLane.one:
                        if (colNumber == 4)
                        {
                            CallTurning(other);
                        }
                        break;
                    case PlayerController.PlayerLane.two:
                        if (colNumber == 3)
                        {
                            CallTurning(other);
                        }
                        break;
                    case PlayerController.PlayerLane.three:

                        if (colNumber == 2)
                        {
                            CallTurning(other);
                        }
                        break;
                    case PlayerController.PlayerLane.four:
                        if (colNumber == 1)
                        {
                            CallTurning(other);
                        }
                        break;
                    default:
                        break;
                }

                
            }


            if (turnOnce && (InputController.Static.turnSide != InputController.TurnSide.none))
            {
                //Debug.Log("trigger small turn collider");
                //// perform turning based on where i entered
                //playerControllerScript.currentTurnLaneBlock = other.transform;
                //playerControllerScript.GetNewStreetTarget(playerControllerScript.intersectionType);
                
            }
        }
    }

    void CallTurning(Collider other)
    {
        Debug.Log(colNumber);
        playerControllerScript.currentTurnLaneBlock = other.transform;
        playerControllerScript.GetNewStreetTarget(playerControllerScript.intersectionType);
        colNumber = 0;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InterZone"))
        {
            colNumber = 0;
            //playerControllerScript.canChangeLane = true;
            //playerControllerScript.canTurn = false;
        }

    }
}
