using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    public Transform camera_axis_central;
    public Transform camera;
    public float camera_speed;
    public float mouse_x;
    public float mouse_y;
    float wheel;


    private void Start()
    {
        
    }

    void CameraMove()
    {
        mouse_x += Input.GetAxis("Mouse X");
        mouse_y += Input.GetAxis("Mouse Y") * -1;

        if (mouse_y > 10) mouse_y = 10;
        if (mouse_y < 0) mouse_y = 0;

        camera_axis_central.rotation = Quaternion.Euler(new Vector3(
            camera_axis_central.rotation.x + mouse_y,
            camera_axis_central.rotation.y + mouse_x, 0) * camera_speed);
    }

    private void Update()
    {
        CameraMove();
    }

}
