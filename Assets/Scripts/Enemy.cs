using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int initialHealth;
    public int health;

    public GameObject deathParticles;
    public GameObject damageParticles = null;

    public float collisionRadius = 0.5f;
    public float collisionRadiusOffsetY = 0f;
    public LayerMask player;

    public GameObject pointsParticle;
    public int points;

    [Header("CameraShake")]
    public float death_shakeDuration = 0.2f;
    public float death_shakeMagnitude = 0.2f;
    public float damage_shakeDuration = 0.05f;
    public float damage_shakeMagnitude = 0.05f;

    void Start()
    {
        health = initialHealth;
    }

    public void takeDamage(int damage, RaycastHit2D hit)
    {
        health -= damage;

        if (health <= 0)
        {
            die();
        }
        else {
            CameraShake.camShake.Shake(damage_shakeDuration, damage_shakeMagnitude);

            if (damageParticles != null)
            {
                GameObject parts = Instantiate(damageParticles);
                parts.transform.position = hit.point;

                float angle = Mathf.Atan2(hit.normal.y, hit.normal.x);
                angle *= 180 / Mathf.PI;
                parts.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
            }
        }
    }

    void die()
    {
        GameObject parts = Instantiate(deathParticles);
        parts.transform.position = transform.position;

        GameObject _points = Instantiate(pointsParticle);
        _points.transform.position = transform.position;

        PointsParticle pScript = _points.GetComponent<PointsParticle>();
        pScript.points = points;

        Score.scoreScript.incrementScore(points);

        CameraShake.camShake.Shake(death_shakeDuration, death_shakeMagnitude);

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector3 colliderPos = new Vector3(transform.position.x, transform.position.y + collisionRadiusOffsetY);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(colliderPos, collisionRadius, player);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Player")
            {
                colliders[i].gameObject.GetComponent<PlayerHealth>().takeDamage();
            }
        }
    }

    public void OnDrawGizmos()
    {
        Vector3 colliderPos = new Vector3(transform.position.x, transform.position.y + collisionRadiusOffsetY);
        Gizmos.DrawWireSphere(colliderPos, collisionRadius);
    }
}
