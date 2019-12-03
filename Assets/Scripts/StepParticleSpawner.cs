using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepParticleSpawner : MonoBehaviour {

    public GameObject StepParticle;
    public AK.Wwise.Event akEvent;
    public Transform RightStep;
    public Transform LeftStep;
    bool PlayL = false;
    bool PlayR = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayL)
        {
            PlayL = false;
            GameObject g = Instantiate(StepParticle, LeftStep.position, Quaternion.identity);
            akEvent.Post(gameObject);
            Destroy(g, 3f);
        }
        if (PlayR)
        {
            PlayR = false;
            GameObject g = Instantiate(StepParticle, RightStep.position, Quaternion.identity);
            akEvent.Post(gameObject);
            Destroy(g, 3f);
        }
	}

    void StepParticleR()
    {
        PlayR = true;
    }

    void StepParticleL()
    {
        PlayL = true;
    }
}
