using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGravity : MonoBehaviour
{
    public GameObject atractor;
    public Rigidbody2D rdb;
    public Vector2 inercia;
    Vector2 dir;
    public float gravityForce = 0.7f;

    void Start()
    {
        rdb.AddForce(inercia);
        atractor = GameObject.Find("planeta");
    }

    private void FixedUpdate()
    {
        dir = atractor.transform.position - transform.position;
        rdb.AddForce((dir.normalized * gravityForce) / dir.magnitude);
    }
}