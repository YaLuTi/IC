using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {

    Animator animator;
    public bool Invulnerability = false; // Remember change this to private

    public GameObject DamagedParticle;

    [Header("Value")]
    public float MaxHP = 10;
    public float HP = 10;
    public Slider HPslider;

    public float MaxSP = 10;
    public float SP = 10;
    public Slider SPslider;
    public float SPRegenSpeed;
    float SPRegenCount;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        HPslider.value = HP / MaxHP;
        SPslider.value = SP / MaxSP;

        if(SPRegenCount > 1.5f && SP < MaxSP)
        {
            SP += 1.5f * Time.deltaTime;
            if(SP >= MaxSP)
            {
                SP = MaxSP;
            }
        }else if (SPRegenCount < 1.5f)
        {
            SPRegenCount += 1 * Time.deltaTime;
        }
    }

    public void Damaged(float damage)
    {
        if (Invulnerability)
        {
            Debug.Log("Dodge");
            return;
        }
        Instantiate(DamagedParticle, transform.position, Quaternion.identity);
        HP -= damage;
        animator.SetTrigger("Damaged");
    }

    public void ExpendSP(float sp)
    {
        if(SP - sp >= 0)
        {
            SP -= sp;
            SPRegenCount = 0;
        }
        else
        {
            SP = 0;
            SPRegenCount = 0;
        }
    }

    public bool CheckSP(float sp)
    {
        if (SP - sp >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool OverpullSP()
    {
        if (SP > 0)
        {
            SPRegenCount = 0;
            SP = 0;
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
