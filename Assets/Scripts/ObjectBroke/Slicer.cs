using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour {

    public GameObject BreakObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Weapon"))
        {
            Instantiate(BreakObject, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
