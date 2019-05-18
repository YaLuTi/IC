using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {

    Animator animator;
    public bool Invulnerability = false; // Remember change this to private
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damaged()
    {
        if (Invulnerability)
        {
            Debug.Log("Dodge");
            return;
        }
        animator.SetTrigger("Damaged");
    }

    void SetInvulnerability()
    {
        Invulnerability = true;
    }
    void ReSetInvulnerability()
    {
        Invulnerability = false;
    }
}
