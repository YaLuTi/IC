using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZCameraSwitch : MonoBehaviour {

    public GameObject Player;
    public GameObject MainCamera;
    public GameObject EZCamera;

    bool Switch = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("L3"))
        {
            Debug.Log("?");
            if (Switch)
            {
                Player.SetActive(false);
                MainCamera.SetActive(false);
                EZCamera.gameObject.SetActive(true);
                Switch = !Switch;
            }
            else
            {
                Player.SetActive(true);
                MainCamera.SetActive(true);
                EZCamera.gameObject.SetActive(false);
                Switch = !Switch;
            }
        }
    }
}
