using UnityEngine;

public class Enemy : MonoBehaviour {

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

        if (dist < 120f && dist > 0)
        {
            SetMylsefAsTarget();       
        }

        if (dist < 40f && dist > 0)
        {
            PrepareShooting();
        }

        if (dist < 30f && dist > 0)
        {
            UnsetMyselfAsTargetAndShoot();    
        }
    }

    void SetMylsefAsTarget()
    {
        if (atackScript.currentTarget == null)
        {
            atackScript.currentTarget = gameObject;
            Targeted(true);
        }      
    }

    void UnsetMyselfAsTargetAndShoot()
    {
        atackScript.currentTarget = null;
        Targeted(false);
        Debug.Log("Unset!");
        // shoot the player
        if (numberOfShots > 0)
        {
            //GameObject Obj = Instantiate(projectile, transform.position + new Vector3(0, 0, -5f), Quaternion.identity) as GameObject;
            //Obj.GetComponent<Projectile>().target = player;
            //numberOfShots--;
        }
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

    // powerup weapons and prepare to fire. at this point the player cannot shoot me anymore. initialize warning on player
    void PrepareShooting()
    {
        //disable targeting on me
        atackScript.currentTarget = null;
    }

    // when targeted, display target animation - managed only by the player!
    public void Targeted (bool t)
    {
        if (t)
            targetedNotifier.SetActive(true);
        else
            targetedNotifier.SetActive(false);
    }

    // destroy this object when dead
    void Die()
    {
        //drop a life then die
        Destroy(gameObject);
    }
}
