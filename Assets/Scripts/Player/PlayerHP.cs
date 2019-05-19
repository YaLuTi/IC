using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {

    Animator animator;
    public bool Invulnerability = false; // Remember change this to private

    public float MaxHP = 10;
    public float HP = 10;
    public Slider HPslider;

    public float MaxSP = 10;
    public float SP = 10;
    public Slider SPslider;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        HPslider.value = HP / MaxHP;
        SPslider.value = SP / MaxSP;
    }

    public void Damaged(float damage)
    {
        if (Invulnerability)
        {
            Debug.Log("Dodge");
            return;
        }
        HP -= damage;
        animator.SetTrigger("Damaged");
    }

    public bool ExpendSP(float sp)
    {
        if(SP - sp >= 0)
        {
            SP -= sp;
            return true;
        }
        else
        {
            return false;
        }
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
