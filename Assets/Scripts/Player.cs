using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;

    Vector3 forward; // Eixo Z
    Vector3 strafe; // Eixo X
    Vector3 vertical; // Eixo Y

    float forwardSpeed = 5f;
    float strafeSpeed = 5f;

    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //gravity = (-2*Hmax) / t^2
        //jumpSpeed = (2*Hmax) / t
        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        //force = input * speed * direction
        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;

        vertical += gravity * Time.deltaTime * Vector3.up;
        if(controller.isGrounded){
            vertical = Vector3.down;
        }
        if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded){
            vertical = jumpSpeed * Vector3.up;
        }

        Vector3 finalVelocity = forward + strafe + vertical;

        controller.Move(finalVelocity * Time.deltaTime);
    }
}
