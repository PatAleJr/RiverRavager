using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    public float limit1;
    public float limit2;
    public float limitWater;

    public float speed1;
    public float speed2;
    public float speedWater;

    public float BG1width;
    public float BG2width;
    public float waterWidth;

    public Transform[] BG1;
    public Transform[] BG2;
    public Transform[] Water;

    // Update is called once per frame
    void Update()
    {
        foreach (Transform BG in BG1)
        {
            Vector3 moveVec = new Vector3(-speed1 * Time.deltaTime, 0f, 0f);
            BG.Translate(moveVec);
        }
        foreach (Transform BG in BG2)
        {
            Vector3 moveVec = new Vector3(-speed2 * Time.deltaTime, 0f, 0f);
            BG.Translate(moveVec);
        }
        foreach (Transform water in Water)
        {
            Vector3 moveVec = new Vector3(-speedWater * Time.deltaTime, 0f, 0f);
            water.Translate(moveVec);
        }

        if (BG1[0].position.x < limit1)
        {
            BG1[0].position = new Vector3(BG1[BG1.Length-1].position.x + BG1width, 1f, 0f);

            //Shifts the elements of the array
            Transform temp = BG1[0];
            for (int i = 1; i < BG1.Length; i++)
                BG1[i - 1] = BG1[i];
            BG1[BG1.Length - 1] = temp;
        }

        if (BG2[0].position.x < limit1)
        {
            BG2[0].position = new Vector3(BG2[BG2.Length - 1].position.x + BG2width, 1f, 0f);
            BG2[0].localScale *= new Vector2(-1f, 1f);
            //Shifts the elements of the array
            Transform temp = BG2[0];
            for (int i = 1; i < BG2.Length; i++)
                BG2[i - 1] = BG2[i];
            BG2[BG1.Length - 1] = temp;

        }
        if (Water[0].position.x < limitWater)
        {
            Water[0].position = new Vector3(Water[Water.Length - 1].position.x + waterWidth, -2.95f, 0f);
            Water[0].localScale *= new Vector2(-1f, 1f);
            //Shifts the elements of the array
            Transform temp = Water[0];
            for (int i = 1; i < Water.Length; i++)
                Water[i - 1] = Water[i];
            Water[Water.Length - 1] = temp;

        }
    }
}
