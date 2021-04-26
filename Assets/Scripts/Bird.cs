using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float Xspeed;
    public float Yspeed;

    public float mapWidth;
    public float mapHeight;

    private bool right;

    private float startX;
    private float startY;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        startX = -mapWidth * 0.6f;
        startY = Random.Range(-mapHeight*0.4f, mapHeight*0.2f);

        right = (Random.Range(0, 1f) < 0.5f);

        if (right)
        {
            transform.position = new Vector3(startX, startY, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.position = new Vector3(-startX, startY, 0f);
            Xspeed *= -1;
        }
    }

    void Update()
    {
        if ((right && transform.position.x > mapWidth * 0.6f) || (!right && transform.position.x < -mapWidth * 0.6f))
            Destroy(gameObject);

        float xx = Xspeed * Time.deltaTime;
        float yy;

        if (player.transform.position.y - transform.position.y > 0.1f)  //Player above bird
        {
            yy = Yspeed * Time.deltaTime;
        } else if (player.transform.position.y - transform.position.y < -0.1f)  //Bird above player
        {
            yy = -Yspeed * Time.deltaTime;
        } else
        {
            yy = 0f;
        }

        Vector3 move = new Vector3(xx, yy, 0f);
        transform.position += move;
    }
}
