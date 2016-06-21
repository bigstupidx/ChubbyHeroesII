using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { idle, shooting, dropping};
    public EnemyType enemyType;
    public GameObject targetedNotifier;
    public GameObject projectile;

    GameObject player;
    PlayerController playerScript;
    AttackScript atackScript;

    int numberOfShots = 1;
    float dist;
    int health = 3;

    // find the player and associated scripts
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        atackScript = player.GetComponent<AttackScript>();
        InvokeRepeating("AutoTarget", 1f, 0.1f);
    }

    void AutoTarget()
    {
        if (player == null)
            return;

        if (health <= 0)
        {
            Die();
        }

        dist = transform.position.z - player.transform.position.z;

        switch (enemyType)
        {
            case EnemyType.idle:
                if (dist < 30f && dist > 0)
                {
                    if (atackScript.currentTarget == gameObject)
                        UnsetMyselfAsTarget();
                }
                else if (dist < 120f && dist > 30)
                {
                    SetMylsefAsTarget();
                }
                break;

            case EnemyType.shooting:
                if (dist < 60f && dist > 0)
                {
                    if (atackScript.currentTarget == gameObject)
                    {
                        UnsetMyselfAsTarget();
                        InvokeRepeating("Shoot", 0.1f, 0.1f);           
                    }
                }
                else if (dist < 120f && dist > 60)
                {
                    SetMylsefAsTarget();
                }
                break;

            case EnemyType.dropping:
                if (dist < 70f && dist > 0)
                {
                    if (atackScript.currentTarget == gameObject)
                    {
                        UnsetMyselfAsTarget();
                        DropBomb();
                    }       
                }
                else if (dist < 120f && dist > 60)
                {
                    SetMylsefAsTarget();
                }
                break;

            default:
                break;
        }
    }

    // ........................................................

    void SetMylsefAsTarget()
    {
        if (atackScript.currentTarget == null)
        {
            atackScript.currentTarget = gameObject;
            targetedNotifier.SetActive(true);
        }      
    }

    void Shoot()
    {
            GameObject Obj = Instantiate(projectile, transform.position + new Vector3(0, 0, -5f), Quaternion.identity) as GameObject;
    }

    void DropBomb()
    {
        if (numberOfShots > 0)
        {
            GameObject Obj = Instantiate(projectile, transform.position + new Vector3(0, 0, -5f), Quaternion.identity) as GameObject;
            Obj.GetComponent<ProjectileEnemyBombDropper>().target = player;
            numberOfShots--;
        }
    }

    void UnsetMyselfAsTarget()
    {
        atackScript.currentTarget = null;
        targetedNotifier.SetActive(false);
    }

    // destroy this object when dead
    void Die()
    {
        atackScript.currentTarget = null;
        //drop a life then die
        Destroy(gameObject);
    }

    // receive dmage from bullets and destroy bullets
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("I got shot!");
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            health--;
        }
    }
}
