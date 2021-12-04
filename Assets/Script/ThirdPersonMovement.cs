using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;   
    float horizontal;
    float vertical;    
    public float speed = 12f;
    public float turn_smooth_time = 0.1f;    
    float turn_smoot_velocity;    
    Vector3 direction;

    public float gravity = -9.81f;
    Vector3 gravity_velocity;

    public Transform ground_check;
    public float ground_distance = 0.4f;
    public LayerMask ground_mask;
    bool is_grounded;

    public float jump_height = 3f;

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        GetInput();
        Move();
        Jump();        
        Gravity();
    }


    void IsGrounded()
    {
        is_grounded = Physics.CheckSphere(ground_check.position, ground_distance, ground_mask);
        if (is_grounded && gravity_velocity.y < 0)
        {
            gravity_velocity.y = -2f;
        }
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {            
            float target_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turn_smoot_velocity, turn_smooth_time);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, target_angle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);            
            //controller.Move(direction * speed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            gravity_velocity.y = Mathf.Sqrt(jump_height * -2f * gravity);
        }
    }

    void Gravity()
    {
        gravity_velocity.y += gravity * Time.deltaTime;
        controller.Move(gravity_velocity * Time.deltaTime);
    }
}
