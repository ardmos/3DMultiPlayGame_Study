using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_mech_controller : MonoBehaviour
{
    public enum AttackState
    {
        Attack0,
        Attack1,
        Attack2,
        Attack3
    }
    public AttackState next_attack_type;   //연속 공격을 위한 현재 공격 상태 체크 enum
    float move_power;   //이동의 빠르기를 조절하는 변수
    float h_axis, v_axis;   //Input Axis 값을 받기 위한 변수
    Vector3 move_vec;   //위 두 개를 합쳐서 만든 변수
    Animator animator;

    bool jump_down;
    bool attack_down;



    private void Awake()
    {
        move_power = 15f;
        animator = GetComponentInChildren<Animator>();
        next_attack_type = AttackState.Attack0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Jump();
        Attack();
    }



    void GetInput()
    {
        h_axis = Input.GetAxisRaw("Horizontal");
        v_axis = Input.GetAxisRaw("Vertical");
        //Debug.Log(h_axis + ", " + v_axis);
    }

    void Move()
    {
        animator.SetFloat("Speed", GetAxisValue());    //달리기 애니메이션 재생을 위해 파라미터 전달.
        move_vec = new Vector3(h_axis, 0, v_axis).normalized;   //대각선 이동시 속도가 빨라지는걸 막아주기 위해. 어떤 값이든 방향이 1로 보정되는 벡터. normalized를 사용.
        transform.position += move_vec * move_power * Time.deltaTime;    //실제 이동. transform 이동은 항상 Time.deltaTime까지. speed는 인스펙터상에서 정의.
        transform.LookAt(transform.position + move_vec);    //나아가는 방향을 바라보기. 
    }

    float GetAxisValue()   //h 이동 v 이동 모두 포함해서 이동중인지 확인하기 위한 메서드.
    {
        float return_axis = 0f;
        if (h_axis != 0f) return_axis = h_axis;
        if(v_axis != 0f) return_axis = v_axis;
        return Mathf.Abs(return_axis);
    }

    void Jump()
    {

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

    void LookAround()
    {

    }
}
