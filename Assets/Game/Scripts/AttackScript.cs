using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    PlayerController playerScript;
    public GameObject projectile;
    int countProjectile;
    public Transform shootPos;
    public GameObject currentTarget;
    float attackRange = 100;

    // Use this for initialization
    void Start ()
    {
        playerScript = GetComponent<PlayerController>();

        //InvokeRepeating("FindClosestEnemy", 0.1f, 0.1f);
    }


    public void Shoot()
    {
        if (Time.timeScale != 1)
            return;

        if ((playerScript.CurrentState == PlayerStates.PlayerAlive || playerScript.CurrentState == PlayerStates.powerJump) && currentTarget != null)
        {
            // really shoot a bullet
            playerScript.playerAnimator.SetTrigger("attack");
            SoundController.Static.playSoundFromName("Shuriken");
            GameObject Obj = Instantiate(projectile, shootPos.position, Quaternion.identity) as GameObject;

            //Debug.Log("Bullet to target : " + currentTarget.transform.parent.name);
            Obj.GetComponent<Projectile>().target = currentTarget;
            countProjectile++;
        }
    }

    //public void FindClosestEnemy()
    //{
    //    // get all enemies in the scene
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    //    // check if we have some enemies to work with, else return null GameObject 
    //    if (enemies.Length == 0)
    //    {
    //        currentTarget = null;
    //        return;
    //    }

    //    float maxDistance = 120f;
    //    float tempDistance;

    //    // find closest enemy to player on the Z axis
    //    for (int i = 0; i < enemies.Length; i++)
    //    {
    //        tempDistance = Vector3.Distance(transform.position, enemies[i].transform.position);

    //        if (tempDistance < maxDistance )
    //        {
    //            if(enemies[i].GetComponent<Enemy>().targetable)
    //            {
    //                maxDistance = tempDistance;
    //                currentTarget = enemies[i];
    //            }                  
    //        } 
    //    }

    //    // call Targeted Method on closest enemy
    //    if(currentTarget != null)
    //    {
    //        currentTarget.GetComponent<Enemy>().Targeted(true);
    //        Debug.Log("Current target is : " + currentTarget.transform.parent.name);
    //    }


    //}


    // clear current target when enemy 

}


