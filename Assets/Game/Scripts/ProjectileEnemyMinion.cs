﻿using UnityEngine;
using System.Collections;

public class ProjectileEnemyMinion : MonoBehaviour
{

    Vector3 targetPos;

    // Use this for initialization
    void Start()
    {
        Invoke("SelfDetruct", 2f);
        targetPos = new Vector3(transform.position.x, -10f, transform.position.z - 30f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 60f); 
    }

    void SelfDetruct()
    {
        Destroy(gameObject);
    }
}