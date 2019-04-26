using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBrokeSFX : MonoBehaviour {

    public AudioAssets assets;
	// Use this for initialization
	void Start () {
        AudioSource audio = GetComponent<AudioSource>();
        assets.Play(audio);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
