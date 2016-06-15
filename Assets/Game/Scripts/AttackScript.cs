using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    PlayerController playerScript;
    public GameObject projectile;
    int countProjectile;
    Vector3 enemyPos;

    // Use this for initialization
    void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //InvokeRepeating("FindClosestEnemy", 1f, 1f);
    }

    GameObject FindClosestEnemy()
    {
        Debug.Log("trying to find enemy");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int closestIndex = 0;
        float closestDistance = Mathf.Infinity;
        float tempDistance;
        for (int i = 0; i < enemies.Length; i++)
        {
            tempDistance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (tempDistance < closestDistance)
            {
                closestDistance = tempDistance;
                closestIndex = i;
            }
        }

        return enemies[closestIndex];
    }

    public void Shoot()
    {
        //stopTutorial = true;
        if ((playerScript.CurrentState == PlayerStates.PlayerAlive || playerScript.CurrentState == PlayerStates.powerJump) && FindClosestEnemy() != null)
        {
            if (Time.timeScale != 1)
            return;

            playerScript.playerAnimator.SetTrigger("attack");
            SoundController.Static.playSoundFromName("Shuriken");

            //Vector3 bulletDirection = FindClosestEnemy().transform.position - transform.position;
            //Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection, Vector3.up);

            GameObject Obj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            Obj.GetComponent<Projectile>().target = FindClosestEnemy();
            countProjectile++;
        }
    }
}


