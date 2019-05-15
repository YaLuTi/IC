using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemParticle : MonoBehaviour {

    public GameObject JumpSlashParticle;
    public Transform JumpSlashParticleTransform;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayJumpSlashParticle()
    {
        Quaternion r = JumpSlashParticleTransform.rotation;
        r.x = 0;
        r.z = 0;
        Vector3 p = JumpSlashParticleTransform.position;
        p.y = 6.4f;
        Debug.Log(p);
        Instantiate(JumpSlashParticle, p, r);
    }
}
