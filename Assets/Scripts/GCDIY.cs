using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCDIY : MonoBehaviour {

    public float FPS;
	// Use this for initialization
	void Start () {
        System.GC.Collect();
	}
	
	// Update is called once per frame
	void Update () {
        FPS = 1.0f / Time.deltaTime;

    }
}
