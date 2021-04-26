using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour
{
    public float startY = -7f;

    public float speedY = 4f;
    public float gravity = -2.7f;

    public float minY;

    private float startTime = 0f;

    void Start()
    {
        float y = startY;

        transform.position = new Vector3(transform.position.x, y, 0f);

        startTime = Time.time;
    }


    void Update()
    {
        float t = Time.time - startTime;
        float yy = startY + (t * speedY) + (t * t * gravity * 0.5f);
        transform.position = new Vector3(transform.position.x, yy, 0f);
    }
}
