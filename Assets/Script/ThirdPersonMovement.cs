using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public enum AttackState
    {
        Attack0,
        Attack1,
        Attack2,
        Attack3
    }
    public AttackState next_attack_type;   //연속 공격을 위한 현재 공격 상태 체크 enum

    public CharacterController controller;
    public Transform cam;   
    float horizontal;
    float vertical;    
    public float speed = 12f;
    public float turn_smooth_time = 0.02f;    
    float turn_smoot_velocity;    
    Vector3 direction;

    public float gravity = -9.81f;
    Vector3 gravity_velocity;

    public Transform ground_check;
    public float ground_distance = 0.4f;
    public LayerMask ground_mask;
    bool is_grounded;

    public float jump_height = 3f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        GetInput();
        Move();
        Jump();        
        Gravity();
        Attack();
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
        float direction_magnitude = direction.magnitude;
        animator.SetFloat("direction_magnitude", direction_magnitude);
        if (direction_magnitude >= 0.1f)
        {            
            float target_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turn_smoot_velocity, turn_smooth_time);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, target_angle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);                        
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

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (next_attack_type)
            {
                case AttackState.Attack0:
                    //콤보 시작
                    animator.SetTrigger("Attack0");
                    next_attack_type = AttackState.Attack1;
                    break;
                case AttackState.Attack1:
                    animator.SetTrigger("Attack0");
                    next_attack_type = AttackState.Attack2;
                    break;
                case AttackState.Attack2:
                    animator.SetTrigger("Attack0");
                    next_attack_type = AttackState.Attack3;
                    break;
                case AttackState.Attack3:
                    animator.SetTrigger("Attack0");
                    next_attack_type = AttackState.Attack0;
                    //끝
                    break;
                default:
                    Debug.Log("공격상태가 이상합니다. ${attack_state}");
                    break;
            }
        }
    }
}
