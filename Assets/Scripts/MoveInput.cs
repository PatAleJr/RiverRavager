using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInput : MonoBehaviour
{
    CharacterController controller;

    public bool sprinting = false;

    public float runSpeed = 20f;
    public float sprintSpeed = 30f;
    bool jump = false;

    public float horizontalMove = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float speed = runSpeed;
        if (Input.GetButton("Sprint"))
            speed = sprintSpeed;

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetAxisRaw("Jump") == 1)
        {
            jump = true;
        }
    }

    public void OnLanding()
    {
        //Animation?
    }

    private void FixedUpdate()
    {
        //Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
