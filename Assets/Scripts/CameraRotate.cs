using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    public GameObject followObj;
    Vector3 distance;
    public float smoothTime = 0.3f;
    public float rotateSpeed = 3f;
    public float Xangle = 0.5f;
    public float Yangle = 0.5f;
    private Vector3 velocity = Vector3.zero;
    Vector3 rotateVector = new Vector3();

    void Start ()
    {
        if (followObj == null)
        {
            followObj = GameObject.FindGameObjectWithTag("Player");
        }
        distance = followObj.transform.position - this.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float h = Input.GetAxis("CameraHorizontal");
        float v = Input.GetAxis("CameraVertical");

        transform.position = Vector3.SmoothDamp(transform.position, followObj.transform.position + (rotateVector * 3), ref velocity, smoothTime);
        
        transform.LookAt(followObj.transform);
        Xangle += h * rotateSpeed * Time.deltaTime;
        Yangle += v * rotateSpeed * Time.deltaTime / 2;
        Yangle = Mathf.Min(1, Yangle);
        Yangle = Mathf.Max(0, Yangle);
        rotateVector = new Vector3(Mathf.Sin(Xangle), Yangle, Mathf.Cos(Xangle));
    }
}
