using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool targetable = true;
    public GameObject targetedNotifier;
    public GameObject projectile;
    GameObject player;
    PlayerController playerScript;
    AttackScript atackScript;
    int numberOfShots = 1;
    float dist;

    int health = 3;

    // find the player
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        atackScript = player.GetComponent<AttackScript>();
    }

    // check if still alive and check if I should shoot
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        if (player == null)
            return;
        dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < 60f)
        {
            PrepareShooting();
        }

        if (dist < 40f)
        {
            if (numberOfShots > 0)
            {
                Shoot();
                numberOfShots--;
            }
        }


    }

    // receive dmage from bullets and destroy bullets
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I got shot!");
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            health--;
        }
    }

    void PrepareShooting()
    {
        //disable targeting on me
        atackScript.currentTarget = null;
        atackScript.FindClosestEnemy();
        targetable = false;
        targetedNotifier.SetActive(false);
        Debug.Log("Weapons Armed!");
    }

    // shoot at the player when in range
    void Shoot()
    {
        
        GameObject Obj = Instantiate(projectile, transform.position + new Vector3(0,0,-5f), Quaternion.identity) as GameObject;
        Obj.GetComponent<Projectile>().target = player;
    }

    // when tageted, display target animation
    public void Targeted()
    {
        targetedNotifier.SetActive(true);
    }

    // destroy this object when dead
    void Die()
    {
        //drop a life then die
        Destroy(gameObject);
    }
}
