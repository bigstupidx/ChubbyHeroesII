using UnityEngine;
using System.Collections;

public class ProjectileEnemyBombDropper : MonoBehaviour
{

    public GameObject player;
    public GameObject bomb;
    Vector3 targetPos;
    float offset = 30f;
    PlayerController playerController;


    // Use this for initialization
    void Start()
    {
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            //targetPos = new Vector3(player.transform.position.x, 0f, player.transform.position.z + offset); // lane for x?
            switch (playerController.currentLane)
            {
                case PlayerController.PlayerLane.one:
                    targetPos = new Vector3 (playerController.LanesPositions[0], 0f, player.transform.position.z + 30f) ;
                    break;
                case PlayerController.PlayerLane.two:
                    targetPos = new Vector3(playerController.LanesPositions[1], 0f, player.transform.position.z + 30f);
                    break;
                case PlayerController.PlayerLane.three:
                    targetPos = new Vector3(playerController.LanesPositions[2], 0f, player.transform.position.z + 30f);
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 50f);
        }
        else
            SelfDetruct();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I collided with : " + other.name);
        if (other.gameObject.CompareTag("Ground"))
        {
            bomb.SetActive(true);
            bomb.transform.SetParent(null);
            Destroy(gameObject);
        }
    }

    void SelfDetruct()
    {
        Destroy(gameObject);
    }
}
