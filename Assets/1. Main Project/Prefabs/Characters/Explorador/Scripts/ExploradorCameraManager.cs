using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorCameraManager : MonoBehaviour

{

    public Transform PlayerBody;
    public float MouseSensitivity;

    public float Smooth;

    float XAxisClamp;

    void Start()

    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()

    {

        RotateCamera();

    }

    void RotateCamera()

    {

        float Mouse_X = Input.GetAxis("Mouse X");
        float Mouse_Y = Input.GetAxis("Mouse Y");

        float Rotation_Amount_X = Mouse_X * MouseSensitivity * Smooth;
        float Rotation_Amount_Y = Mouse_Y * MouseSensitivity * Smooth;

        XAxisClamp -= Rotation_Amount_Y;

        Vector3 Target_Rotation_Cam = transform.rotation.eulerAngles;
        Vector3 Target_Rotation_Body = PlayerBody.rotation.eulerAngles;

        Target_Rotation_Cam.x -= Rotation_Amount_Y;
        Target_Rotation_Cam.z = 0;
        Target_Rotation_Body.y += Rotation_Amount_X;

        if (XAxisClamp > 90)

        {

            XAxisClamp = 90;
            Target_Rotation_Cam.x = 90;

        }

        else if (XAxisClamp < -90)

        {

            XAxisClamp = -90;
            Target_Rotation_Cam.x = 270;

        }


        transform.rotation = Quaternion.Euler(Target_Rotation_Cam);
        PlayerBody.rotation = Quaternion.Euler(Target_Rotation_Body);

    }

}