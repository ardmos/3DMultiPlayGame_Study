using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_mech_controller : MonoBehaviour
{
    float move_speed;   //실제 이동 속도
    float h_axis, v_axis;   //Input Axis 값을 받기 위한
    Vector3 move_vec;   //위 두 개를 합쳐서 만든
    Animator animator;

    bool j_down, is_jump;



    // Start is called before the first frame update
    void Start()
    {
        move_speed = 15f;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h_axis = Input.GetAxisRaw("Horizontal");
        v_axis = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Speed());    //달리기 애니메이션 재생을 위해 파라미터 전달.
        move_vec = new Vector3(h_axis, 0, v_axis).normalized;   //대각선 이동시 속도가 빨라지는걸 막아주기 위해. 어떤 값이든 방향이 1로 보정되는 벡터. normalized를 사용.
        transform.position += move_vec * move_speed * Time.deltaTime;    //실제 이동. transform 이동은 항상 Time.deltaTime까지. speed는 인스펙터상에서 정의.
        transform.LookAt(transform.position + move_vec);    //나아가는 방향을 바라보기. 

        if (Input.GetButtonDown("Jump")) animator.SetBool("Jump", true);
        else if (Input.GetButtonUp("Jump")) animator.SetBool("Jump", false);

        if (Input.GetKeyDown(KeyCode.Mouse0)) animator.SetTrigger("Attack");

    }

    float Speed()
    {
        if (h_axis >= v_axis) return h_axis;
        else return v_axis;
    }

    void GetInput()
    {

    }

    void Move()
    {

    }

    void Turn()
    {

    }

    void Jump()
    {

    }
}
