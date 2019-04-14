using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZCamera : MonoBehaviour {
    public float moveSpeed = 0.1f;
    public float RotateSpeed = 1f;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 hh = Input.GetAxis("Horizontal") * transform.right;
        Vector3 vv = Input.GetAxis("Vertical") * transform.forward;
        float yy = 0;
        if (Input.GetButton("JoyStickX"))
        {
            yy = 1;
        }
        if (Input.GetButton("JoyStickTriangle"))
        {
            yy = -1;
        }

        transform.position += (hh + vv + new Vector3(0, yy, 0)) * moveSpeed;

        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        Vector3 e = transform.eulerAngles;
        e += new Vector3(-v, h, 0) * RotateSpeed;
        transform.eulerAngles = e;
    }
}
