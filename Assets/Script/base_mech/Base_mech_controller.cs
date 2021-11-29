using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_mech_controller : MonoBehaviour
{
    public float speed;
    float h_axis, v_axis;   //Input Axis 값을 받기 위한
    Vector3 move_vec;   //위 두 개를 합쳐서 만든


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h_axis = Input.GetAxisRaw("Horizontal");
        v_axis = Input.GetAxisRaw("Vertical");

        move_vec = new Vector3(h_axis, 0, v_axis).normalized;   //대각선 이동시 속도가 빨라지는걸 막아주기 위해. 어떤 값이든 방향이 1로 보정되는 벡터. normalized를 사용.

        transform.position += move_vec * speed * Time.deltaTime;    //transform 이동은 항상 Time.deltaTime까지. speed는 인스펙터상에서 정의.

    }
}
