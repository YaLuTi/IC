using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderBasic : MonoBehaviour {

    bool Attacking = false;

    public float AttackDamage = 1f;

    [SerializeField]
    List<GameObject> hitObject = new List<GameObject>();

    Collider collider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void StartAttack()
    {
        Attacking = true;
    }

    public void StopAttack()
    {
        hitObject.Clear();
        Attacking = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Attacking && !hitObject.Contains(other.gameObject))
        {
            hitObject.Add(other.gameObject);

            if (other.tag == "Monster")
            {
                MonsterBasic monster = other.gameObject.GetComponent<MonsterBasic>();
                monster.Damaged(AttackDamage);
                Debug.Log("Hit");
            }
        }
    }
}
