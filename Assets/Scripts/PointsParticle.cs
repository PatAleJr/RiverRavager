using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsParticle : MonoBehaviour
{
    public float risingSpeed;
    private TextMeshProUGUI scoreText;
    public int points;

    public float duration;

    void Start()
    {
        scoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreText.text = points.ToString();

        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.position += new Vector3(0f, risingSpeed * Time.deltaTime, 0f);
    }

}
