using UnityEngine;
using System.Collections;

public class ProjectileEnemyBombDropper : MonoBehaviour
{

    public GameObject target;
    public GameObject bomb;
    Vector3 targetPos;
    float offset = 30f;


    // Use this for initialization
    void Start()
    {
        if (target != null)
            targetPos = new Vector3(target.transform.position.x, 0f, target.transform.position.z + offset); // lane for x?
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
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
