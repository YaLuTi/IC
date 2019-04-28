using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour {

    public GameObject StepParticle;
    public Transform RightStep;
    public Transform LeftStep;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StepParticleR()
    {
        GameObject g = Instantiate(StepParticle, RightStep.position, RightStep.rotation);
        Destroy(g, 3f);
    }

    void StepParticleL()
    {
        GameObject g = Instantiate(StepParticle, LeftStep.position, LeftStep.rotation);
        Destroy(g, 3f);
    }
}
