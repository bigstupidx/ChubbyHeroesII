using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    PlayerController playerScript;
    public GameObject projectile;
    int countProjectile;

    // Use this for initialization
    void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        InvokeRepeating("FindClosestEnemy", 0.5f, 0.1f);
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int closestIndex = 0;
        float closestDistance = 120f;
        float tempDistance;

        for (int i = 0; i < enemies.Length; i++)
        {
            //tempDistance = Vector3.Distance(transform.position, enemies[i].transform.position);
            tempDistance = (enemies[i].transform.position.z - transform.position.z);
            //Debug.Log("Object " + enemies[i].name + " is at position " + enemies[i].transform.position.z +  " and it's tempDistance is :" + tempDistance);

            if (Mathf.Abs(tempDistance) < closestDistance && enemies[i].transform.position.z >= (transform.position.z + 30f))
            {
                closestDistance = Mathf.Abs(tempDistance);
                closestIndex = i;
            }
        }
        enemies[closestIndex].GetComponent<Enemy>().Targeted();
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
                GameObject Obj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                Obj.GetComponent<Projectile>().target = firstEnemy;
                countProjectile++;
            }
            //else if (firstEnemy.transform.position.z < transform.position.z + 30f)
            //{
            //    return;
            //}



            //Vector3 bulletDirection = FindClosestEnemy().transform.position - transform.position;
            //Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection, Vector3.up);


        }
    }
}


