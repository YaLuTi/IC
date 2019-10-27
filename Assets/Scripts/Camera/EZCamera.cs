using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZCamera : MonoBehaviour {
    [Header("Speed")]
    public float moveSpeed = 0.1f;
    public float RotateSpeed = 1f;
    public float UpDownSpeed = 0.5f;

    float MoveSpeedMultiplier = 0.5f;

    Camera camera;
    [Header("Value")]
    public float MinFieldOfView;
    public float ViewMultiplier = 1;
    float MaxFieldOfView;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponent<Camera>();
        MaxFieldOfView = camera.fieldOfView;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 hh = Input.GetAxis("Horizontal") * transform.right;
        Vector3 vv = Input.GetAxis("Vertical") * transform.forward;
        float yy = 0;

        if (Input.GetButton("Fire2"))
        {
            if (camera.fieldOfView > MinFieldOfView) camera.fieldOfView -= (camera.fieldOfView - MinFieldOfView) * 0.15f * ViewMultiplier;
        }
        else
        {
            if (camera.fieldOfView < MaxFieldOfView) camera.fieldOfView += (MaxFieldOfView - camera.fieldOfView) * 0.25f * ViewMultiplier;
        }

        if (Input.GetButton("Jump"))
        {
            yy = UpDownSpeed;
        }
        if (Input.GetButton("Down"))
        {
            yy = -UpDownSpeed;
        }
        if (Input.GetButton("Fire3"))
        {
            MoveSpeedMultiplier = 0.5f;
        }
        else
        {
            MoveSpeedMultiplier = 1;
        }

        transform.position += (hh + vv + new Vector3(0, yy, 0)) * moveSpeed * MoveSpeedMultiplier;

        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        Vector3 e = transform.eulerAngles;
        e += new Vector3(-v, h, 0) * RotateSpeed;
        transform.eulerAngles = e;
    }
}
