using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    PlayerController playerScript;
    public GameObject projectile;
    int countProjectile;
    public Transform shootPos;

    // Use this for initialization
    void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        InvokeRepeating("FindClosestEnemy", 0.5f, 0.2f);
    }

    GameObject FindClosestEnemy()
    {
        // get all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // check if we have some enemies to work with, else return null GameObject 
        if (enemies.Length == 0)
            return null;

        int closestIndex = 0;
        float closestDistance = 120f;
        float tempDistance;

        // find closest enemy to player on the Z axis
        for (int i = 0; i < enemies.Length; i++)
        {
            tempDistance = (enemies[i].transform.position.z - transform.position.z);

            if (Mathf.Abs(tempDistance) < closestDistance && enemies[i].transform.position.z >= (transform.position.z + 30f))
            {
                closestDistance = Mathf.Abs(tempDistance);
                closestIndex = i;
            }
        }

        // call Targeted Method on closest enemy
        enemies[closestIndex].GetComponent<Enemy>().Targeted();

        //return the closest enemy gameObject to be used wherever
        return enemies[closestIndex];
    }


    public void Shoot()
    {
        GameObject firstEnemy = FindClosestEnemy();
        
        if ((playerScript.CurrentState == PlayerStates.PlayerAlive || playerScript.CurrentState == PlayerStates.powerJump) && firstEnemy != null)
        {
            if (Time.timeScale != 1)
            return;

            if (firstEnemy.transform.position.z >= (transform.position.z + 30f) && firstEnemy.GetComponent<Enemy>().targetable)
            {
                playerScript.playerAnimator.SetTrigger("attack");
                SoundController.Static.playSoundFromName("Shuriken");
                GameObject Obj = Instantiate(projectile, shootPos.position, Quaternion.identity) as GameObject;
                Obj.GetComponent<Projectile>().target = firstEnemy;
                countProjectile++;
            }
        }
    }
}


