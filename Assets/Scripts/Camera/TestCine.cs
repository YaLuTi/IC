using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCine : MonoBehaviour {

    public Component component;
	// Use this for initialization
	void Start () {
        //StartCoroutine(enumerator());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(8.5f);
        transform.position = new Vector3(0, 0, 0);
        Destroy(component);
        yield return 0;

    }
}
