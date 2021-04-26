using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float jumpForce = 10f;
    public bool isGrounded;
    public LayerMask ground;
    public Transform groundCheck;
    public float groundCastingRadius = 0.01f;

    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    private Vector3 mVelocity = Vector3.zero;
    private bool facingRight = true;
    public Transform characterGFX;  //Seperate object for sprite, so doesnt flip whole object

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public AudioClip jumpNoise;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCastingRadius, ground);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref mVelocity, movementSmoothing);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        if (jump && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            audioSource.clip = jumpNoise;
            audioSource.Play();
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = characterGFX.localScale;
        theScale.x *= -1;
        characterGFX.localScale = theScale;
    }
}
