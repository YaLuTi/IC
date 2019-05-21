using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip clip;
    public MonsterBasic monsterBasic;
    bool IsIn = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (IsIn) return;
        IsIn = true;
        monsterBasic.SetAttack();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
