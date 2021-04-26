using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunTip;
    public GameObject bulletHitParticles;

    public LayerMask bulletHits;

    public Animator shootTipAnimator;

    public AudioClip gunshotNoise;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        lookAtMouse();

        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    void lookAtMouse()
    {
        Vector2 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePosWorld.y - transform.position.y, mousePosWorld.x - transform.position.x);
        float degrees = angle * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, degrees + 90f);
    }

    void Shoot()
    {
        //Effect
        shootTipAnimator.SetTrigger("Shoot");

        //Sound
        audioSource.clip = gunshotNoise;
        audioSource.Play();

        //Recoil?

        //Raycast for enemies
        RaycastHit2D hit = Physics2D.Raycast(gunTip.position, gunTip.right, Mathf.Infinity, bulletHits);
        if (hit.collider != null)
        {
            GameObject hitObj = hit.collider.gameObject;

            //Damage enemies
            if (hitObj.tag == "Enemy")
            {
                Enemy enemy = hitObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(1, hit);
                }
            }

            //Make bullet, blood or water particles depending on what it hit

            //Make bullet particles
            GameObject particles = Instantiate(bulletHitParticles);
            particles.transform.position = hit.point;

            float angle = Mathf.Atan2(hit.normal.y, hit.normal.x);
            angle *= 180 / Mathf.PI;

            particles.transform.rotation = Quaternion.Euler(0, 0, angle-90);
        }
    }
}
