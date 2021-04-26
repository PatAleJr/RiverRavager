using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] Hearts;
    public int initialHealth;
    public int health;

    public AudioClip takeDamageNoise;
    private AudioSource audioSource;

    private Animator playerAnim;

    public bool invincible = false;
    public float invincibilityDuration = 1f;
    private float undoInvincibleTime;

    public GameObject splashParticles;
    public GameObject deathParticles;
    private GameObject gfx;
    private GameObject arm;

    private Vector3 resetPosition;

    public float dieY = -2f;

    [Header("CameraShake")]
    public float death_shakeDuration = 0.5f;
    public float death_shakeMagnitude = 0.3f;
    public float damage_shakeDuration = 0.1f;
    public float damage_shakeMagnitude = 0.1f;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        gfx = transform.Find("CharacterGFX").gameObject;
        arm = transform.Find("Arm").gameObject;

        resetPosition = GameController.gc.playerResetPos.position;

        health = initialHealth;
        foreach (GameObject heart in Hearts)
        {
            heart.SetActive(true);
        }
    }

    public void takeDamage()
    {
        if (invincible || Score.scoreScript.playing == false)
            return;

        if (health > 1)
        {
            Hearts[health-1].SetActive(false);
            health--;

            invincible = true;
            undoInvincibleTime = Time.time + invincibilityDuration;
            playerAnim.SetBool("Invincible", true);

            audioSource.clip = takeDamageNoise;
            audioSource.Play();

            CameraShake.camShake.Shake(damage_shakeDuration, damage_shakeMagnitude);
        }
        else {
            die();
        }
    }

    public void die()
    {
        if (Score.scoreScript.playing == false)
            return;

        CameraShake.camShake.Shake(death_shakeDuration, death_shakeMagnitude);

        GameObject parts = Instantiate(deathParticles);
        parts.transform.position = transform.position;

        arm.SetActive(false);
        gfx.SetActive(false);

        GameController.gc.gameOver();
    }

    public void reset()
    {
        health = initialHealth;
        foreach (GameObject heart in Hearts)
        {
            heart.SetActive(true);
        }

        arm.SetActive(true);
        gfx.SetActive(true);

        transform.position = resetPosition;
    }

    private void Update()
    {
        if (invincible && Time.time >= undoInvincibleTime)
        {
            invincible = false;
            playerAnim.SetBool("Invincible", false);
        }

        if (transform.position.y < dieY)
        {
            if (Score.scoreScript.playing == false)
            {
                transform.position = resetPosition;
            }
            else {
                GameObject parts = Instantiate(splashParticles);
                parts.transform.position = transform.position;
                die();
            }
        }
    }
}
