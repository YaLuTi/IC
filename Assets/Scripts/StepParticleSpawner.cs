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
    int LeftCooldown = 10;
    int RightCooldown = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayL)
        {
            PlayL = false;
            if (LeftCooldown >= 10)
            {
                LeftCooldown = 0;
                GameObject g = Instantiate(StepParticle, LeftStep.position, Quaternion.identity);
                akEvent.Post(gameObject);
                Destroy(g, 3f);
            }
        }
        if (PlayR)
        {
            PlayR = false;
            if (RightCooldown >= 10)
            {
                RightCooldown = 0;
                GameObject g = Instantiate(StepParticle, RightStep.position, Quaternion.identity);
                akEvent.Post(gameObject);
                Destroy(g, 3f);
            }
        }
        LeftCooldown++;
        RightCooldown++;
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
