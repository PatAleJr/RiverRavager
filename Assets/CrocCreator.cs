using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocCreator : MonoBehaviour
{
    public GameObject croc;
    public GameObject splash;

    public float crocDelay;
    private float crocSpawnTime;

    private bool createdCroc = false;

    public float lifeTime = 10f;

    //
    public float minX;
    public float maxX;
    public float startY = -7f;

    void Start()
    {
        float x = Random.Range(minX, maxX);
        float y = startY;

        transform.position = new Vector3(x, y, 0f);

        crocSpawnTime = Time.time + crocDelay;

        Instantiate(splash, transform);
        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        if (Time.time > crocSpawnTime && !createdCroc)
        {
            Instantiate(croc, transform);
            createdCroc = true;
        }
    }
}
