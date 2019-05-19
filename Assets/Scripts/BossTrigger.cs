using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip clip;
    public MonsterBasic monsterBasic;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        monsterBasic.SetAttack();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
