using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour {
    
    public bool Walk = false;
    public bool Rool = false;
    public bool Attack = false;

    public GameObject BreakObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Monster"))
        {
            if (Walk)
            {
                Instantiate(BreakObject, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Rool && collision.gameObject.CompareTag("Player"))
        {
            PlayerMove p = collision.gameObject.GetComponent<PlayerMove>();
            if (p.IsDodging)
            {
                Instantiate(BreakObject, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Attack)
        {
            if (other.gameObject.CompareTag("Weapon"))
            {
                if (other.gameObject.GetComponent<WeaponColliderBasic>().Attacking)
                {
                    Instantiate(BreakObject, transform.position, transform.rotation);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
