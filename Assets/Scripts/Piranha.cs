using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : MonoBehaviour
{
    public GameObject splashParticles;

    public float gravity = -9.8f;  //I can make my own, as long as its constant

    private bool right;

    private float startX;
    private float startY;

    public float speedX;
    private float speedY;

    private float flyTime;

    public float minTravelDistance;

    private float destinationX;
    private float destinationY;

    [Header("Map info")]
    public float mapWidth;
    public float mapHeight;
    public float waterHeight;

    public float boatWidth;
    public float boatBottom;

    private float startTime = 0f;

    void Start()
    {
        startX = Random.Range(-mapWidth / 2, -boatWidth / 2);
        startY = Random.Range(-mapHeight / 2 + waterHeight, -mapHeight / 2 + 0.5f);

        destinationX = Random.Range(startX + minTravelDistance, boatWidth/2);

        flyTime = (destinationX - transform.position.x) / speedX;

        bool endsOnBoat = (destinationX < boatWidth / 2 && destinationX > -boatWidth / 2);

        if (endsOnBoat)
        {
            destinationY = Random.Range(boatBottom, -mapHeight / 2);
        }
        else
        {
            destinationY = Random.Range(-mapHeight / 2 + waterHeight, -mapHeight / 2);
        }

        float dy = startY - destinationY;
        speedY = (dy - (0.5f * gravity * flyTime * flyTime)) / flyTime;

        startTime = Time.time;

        right = (Random.Range(0, 1f) < 0.5f);

        if (right)
        {
            transform.position = new Vector3(startX, startY, 0f);
        }
        else {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.position = new Vector3(-startX, startY, 0f);
        }

        GameObject parts = Instantiate(splashParticles);
        parts.transform.position = transform.position;
    }

    void Update()
    {
        float t = Time.time - startTime;

        float x = startX + t * speedX;
        float y = startY + t * speedY + 0.5f * gravity * t * t;

        if (right)
        {
            transform.position = new Vector3(x, y, 0f);
        }
        else {
            transform.position = new Vector3(-x, y, 0f);
        }

        if (y - destinationY < 0f && t > flyTime-1f)
            disapearIntoWater();
    }

    void disapearIntoWater()
    {
        GameObject parts = Instantiate(splashParticles);
        parts.transform.position = transform.position;
        Destroy(gameObject);
    }
}
