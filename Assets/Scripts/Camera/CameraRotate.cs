using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    public GameObject followObj;
    public GameObject LockObj;

    Collider[] MonstersList;
    float[] distanceList;

    [SerializeField]
    public bool IsLock = false;

    public float value;

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
        if (Input.GetButtonUp("R3"))
        {
            if (IsLock)
            {
                IsLock = false;
            }
            else
            {
                MonstersList = Physics.OverlapSphere(transform.position, 15, LayerMask.GetMask("Creature"));
                distanceList = new float[MonstersList.Length];
                for (int i = 0; i < MonstersList.Length; i++)
                {
                    distanceList[i] = (MonstersList[i].transform.position - transform.position).sqrMagnitude;
                }
                System.Array.Sort(distanceList, MonstersList);

                if (MonstersList.Length > 1)
                {
                    LockObj = MonstersList[1].gameObject;
                    IsLock = !IsLock;
                }
                else
                {
                    float angle = (followObj.transform.eulerAngles.y + 180) / 180 * Mathf.PI;
                    Xangle = angle;
                }
            }
        }
        if (!IsLock)
        {
            NomalCameraMove();
        }
        else
        {
            LockCameraMove();
        }
    }

    void NomalCameraMove()
    {
        float h = Input.GetAxis("CameraHorizontal");
        /*if(h == 0)
        {
            h = Input.GetAxis("Mouse X");
        }*/
        float v = Input.GetAxis("CameraVertical");
        /*if (v == 0)
        {
            v = Input.GetAxis("Mouse Y");
        }*/


        transform.position = Vector3.SmoothDamp(transform.position, followObj.transform.position + (rotateVector * value), ref velocity, smoothTime);
        Quaternion targetRotation = Quaternion.LookRotation(followObj.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.85f);

        Xangle += h * rotateSpeed * Time.deltaTime;
        Xangle %= 6.24f;
        Yangle += v * rotateSpeed * Time.deltaTime / 2;
        Yangle = Mathf.Min(1, Yangle);
        Yangle = Mathf.Max(-1f, Yangle);
        rotateVector = new Vector3(Mathf.Sin(Xangle), Yangle, Mathf.Cos(Xangle));
    }

    void LockCameraMove()
    {
        float h = Input.GetAxis("CameraHorizontal");
        float v = Input.GetAxis("CameraVertical");

        float angle = Mathf.Atan2(LockObj.transform.position.z - followObj.transform.position.z, LockObj.transform.position.x - followObj.transform.position.x) * 180 / Mathf.PI;

        // FIX LATER
        angle = angle / 180 * Mathf.PI;
        angle += Mathf.PI / 2;
        angle *= -1;
        if(angle < 0)
        {
            angle += 6.24f;
        }
        Xangle = angle;

        transform.position = Vector3.SmoothDamp(transform.position, followObj.transform.position + (rotateVector * value), ref velocity, smoothTime);

        transform.LookAt(followObj.transform);
        Yangle = Mathf.Min(1, Yangle);
        Yangle = Mathf.Max(-1f, Yangle);
        rotateVector = new Vector3(Mathf.Sin(angle), 0.1f, Mathf.Cos(angle));
    }
}
