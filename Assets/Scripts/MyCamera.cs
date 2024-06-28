using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public GameObject target;
    public float zoomLimit = 0.0000001f;
    Rigidbody2D rdb;
    Camera cam;

    void Start()
    {
        rdb = target.GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (target)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z) + target.transform.up * rdb.velocity.magnitude * 0.1f;

            float zoom = rdb.velocity.magnitude + 60 * 0.15f;
            zoom = Mathf.Clamp(zoom, 0, zoomLimit);

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.smoothDeltaTime);
        }
    }
}